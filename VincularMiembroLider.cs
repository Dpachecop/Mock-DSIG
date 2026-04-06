using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mock_DSIG
{
    public partial class VincularMiembroLider : Form
    {
        int idLider;
        int idSemilleroLider;
        conexion cn = new conexion();
        public VincularMiembroLider(int idRecibido)
        {
            InitializeComponent();
            this.idLider = idRecibido; 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void ObtenerInv()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // consulta para obtener investigadores sin semillero
                string query = "SELECT idInv AS [ID], nombre_inv AS [Nombre], apellido_inv AS [Apellido]  FROM INVESTIGADOR WHERE SEMILLERO_idSEMILLERO IS NULL AND tipo_inv = 'INVESTIGADOR'";
                SqlDataAdapter da = new SqlDataAdapter(query, conexionAbierta);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridUsuariosAgg.DataSource = dt;
                // llenamos el ComboBox para la seleccion
                comboSeleccionInv.DataSource = dt;
                comboSeleccionInv.DisplayMember = "Nombre";
                comboSeleccionInv.ValueMember = "ID";
                comboSeleccionInv.SelectedIndex = -1; // se coloca menos 1 para que no haya una seleccion por defecto

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar investigadores: " + ex.Message);
            }
        }
        public void ObtenerSemillero()
        {
            try
            {
                // consulta para obtener el semillero del lider
                SqlConnection conexionAbierta = cn.Conectar();
                string query = "SELECT idSEMILLERO FROM SEMILLERO WHERE idInv = @idLider";
                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@idLider", idLider);
                idSemilleroLider = Convert.ToInt32(cmd.ExecuteScalar()); // el execute scalar se usa para obtener un valor unico en este caso el id del semillero del líder
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al identificar semillero: " + ex.Message);
            }
        }
        public void CargarProyectosSemillero()
        {
            try
            {
                // consulta para obtener los proyectos del semillero del lider
                SqlConnection conexionAbierta = cn.Conectar();
                string query = "SELECT idPROYECTO, nombre_proyecto FROM PROYECTO WHERE SEMILLERO_idSEMILLERO = @idSem";

                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@idSem", idSemilleroLider);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                // se llena el combo con los proyectos del semillero
                comboSeleccionProy.DataSource = dt;
                comboSeleccionProy.DisplayMember = "nombre_proyecto";
                comboSeleccionProy.ValueMember = "idPROYECTO";
                comboSeleccionProy.SelectedIndex = -1;
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proyectos: " + ex.Message);
            }
        }
        private void bt_cancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar la vinculación?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btVincularInv_Click(object sender, EventArgs e)
        {
            if (comboSeleccionInv.SelectedValue == null || comboSeleccionProy.SelectedValue == null)
            {
                MessageBox.Show("Por favor seleccione un investigador y un proyecto.");
                return;
            }

            int idInvSeleccionado = Convert.ToInt32(comboSeleccionInv.SelectedValue);
            int idProySeleccionado = Convert.ToInt32(comboSeleccionProy.SelectedValue);

            try
            {
                SqlConnection conexionAbierta = cn.Conectar();

                // vinculamos al investigador con el semillero del líder
                string queryUpdate = "UPDATE INVESTIGADOR SET SEMILLERO_idSEMILLERO = @idSem WHERE idInv = @idInv";
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conexionAbierta);
                cmdUpdate.Parameters.AddWithValue("@idSem", idSemilleroLider);
                cmdUpdate.Parameters.AddWithValue("@idInv", idInvSeleccionado);
                cmdUpdate.ExecuteNonQuery();

                //  vinculamos al investigador con el proyecto específico en la tabla intermedia
                string queryInsert = "INSERT INTO PROYECTO_INVESTIGADOR (idPROYECTO, idINVESTIGADOR) VALUES (@idPROYECTO, @idINVESTIGADOR)";
                SqlCommand cmdInsert = new SqlCommand(queryInsert, conexionAbierta);
                cmdInsert.Parameters.AddWithValue("@idPROYECTO", idProySeleccionado);
                cmdInsert.Parameters.AddWithValue("@idINVESTIGADOR", idInvSeleccionado);
                cmdInsert.ExecuteNonQuery();

                MessageBox.Show("¡Investigador vinculado exitosamente al equipo y al proyecto!", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.Cerrar();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la vinculación: " + ex.Message);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }

        private void VincularMiembroLider_Load(object sender, EventArgs e)
        {
            ObtenerSemillero();
            ObtenerInv();
            CargarProyectosSemillero();
        }
    }
}
