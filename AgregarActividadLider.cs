using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mock_DSIG
{
    public partial class AgregarActividadLider : Form
    {
        conexion cn = new conexion();
        int idProyectoRecibido;
        public AgregarActividadLider(int idProyecto)
        {
            InitializeComponent();
            this.idProyectoRecibido = idProyecto;
        }

        private void AgregarActividadLider_Load(object sender, EventArgs e)
        {
            CargarFasesEnCombo();
        }
        public void CargarFasesEnCombo()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // solo se traen las fases del proyecto seleccionado
                string query = "SELECT idFASES, nombre_fase FROM FASES WHERE PROYECTO_idPROYECTO = @idProy";
                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@idProy", idProyectoRecibido);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "nombre_fase"; // Lo que ve el lider
                comboBox1.ValueMember = "idFASES";       // El ID que guardaremo
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar fases: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void btnAGREGARActividad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDactividad.Text) || string.IsNullOrWhiteSpace(txtNomActividad.Text) || comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Complete el ID, el Nombre y seleccione una Fase.", "Atención");
                return;
            }

            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                string queryValidar = "SELECT COUNT(*) FROM ACTIVIDADES WHERE idACTIVIDADES = @idBusqueda";
                SqlCommand cmdValidar = new SqlCommand(queryValidar, conexionAbierta);
                cmdValidar.Parameters.AddWithValue("@idBusqueda", txtIDactividad.Text);

                if ((int)cmdValidar.ExecuteScalar() > 0)
                {
                    MessageBox.Show("El ID de actividad ya existe. Elija otro.", "ID Duplicado");
                    cn.Cerrar();
                    return;
                }

                string queryInsert = @"INSERT INTO ACTIVIDADES (idACTIVIDADES, FASES_idFASES, nombre_actividad, descripcion_actividad, tipo_actividad, estado_actividad) 
                                     VALUES (@idACTIVIDADES, @FASES_idFASES, @nombre_actividad, @descripcion_actividad, @tipo_actividad, 'Pendiente')";

                SqlCommand cmdInsert = new SqlCommand(queryInsert, conexionAbierta);
                cmdInsert.Parameters.AddWithValue("@idACTIVIDADES", Convert.ToInt32(txtIDactividad.Text));
                cmdInsert.Parameters.AddWithValue("@FASES_idFASES", comboBox1.SelectedValue); // ID de la fase seleccionada
                cmdInsert.Parameters.AddWithValue("@nombre_actividad", txtNomActividad.Text);
                cmdInsert.Parameters.AddWithValue("@descripcion_actividad", txtDescpActividad.Text);
                cmdInsert.Parameters.AddWithValue("@tipo_actividad", txttipoactividad.Text);

                cmdInsert.ExecuteNonQuery();
                cn.Cerrar();

                MessageBox.Show("Actividad registrada con éxito.", "Éxito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void btnCancelarSActividad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar? Se perderán los datos ingresados.", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
