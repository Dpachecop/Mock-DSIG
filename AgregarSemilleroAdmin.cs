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
    public partial class AgregarSemilleroAdmin : Form
    {
        conexion cn = new conexion();
        public AgregarSemilleroAdmin()
        {
            InitializeComponent();
            CargarInvestigadores();
        }
        public void AgregarSemilleros()
        {
            if (string.IsNullOrEmpty(txtNombreSemillero.Text) || comboBoxInvestigadorLider.SelectedValue == null)
            {
                MessageBox.Show("El nombre y el líder son obligatorios.");
                return;
            }

            try
            {
                int idSem = int.Parse(txtidSEMILLERO.Text);
                int idLider = Convert.ToInt32(comboBoxInvestigadorLider.SelectedValue);
                string nombre = txtNombreSemillero.Text;
                string area = txtAreaconocimientosemillero.Text;
                string estado = comboBoxEstadoSemillero.SelectedItem.ToString();
                DateTime fecha = dateTimePickerInicioSemillero.Value;
                int idAdmin = 11;
                SqlConnection conexionAbierta = cn.Conectar();
                string queryInsert = @"INSERT INTO SEMILLERO (idSEMILLERO, idInv, ADMINISTRADOR_idADMINISTRADOR, nombre_semillero, estado_semillero, fecha_creacion, area_conocimiento)  VALUES (@idSEMILLERO, @idInv, @ADMINISTRADOR_idADMINISTRADOR, @nombre_semillero, @estado_semillero, @fecha_creacion, @area_conocimiento)";

                SqlCommand cmdInsert = new SqlCommand(queryInsert, conexionAbierta);
                cmdInsert.Parameters.AddWithValue("@idSEMILLERO", idSem);
                cmdInsert.Parameters.AddWithValue("@idInv", idLider);
                cmdInsert.Parameters.AddWithValue("@ADMINISTRADOR_idADMINISTRADOR", idAdmin);
                cmdInsert.Parameters.AddWithValue("@nombre_semillero", nombre);
                cmdInsert.Parameters.AddWithValue("@estado_semillero", estado);
                cmdInsert.Parameters.AddWithValue("@fecha_creacion", fecha);
                cmdInsert.Parameters.AddWithValue("@area_conocimiento", area);
                cmdInsert.ExecuteNonQuery();
                string ActualizarROL = "UPDATE INVESTIGADOR SET tipo_inv = 'LIDER' WHERE idInv = @idLider";
                SqlCommand cmdUpdate = new SqlCommand(ActualizarROL, conexionAbierta);
                cmdUpdate.Parameters.AddWithValue("@idLider", idLider);

                cmdUpdate.ExecuteNonQuery();

                MessageBox.Show("Semillero Registrado Correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cn.Cerrar();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la operación: " + ex.Message);
                cn.Cerrar();
            }
        }
        
        public void CargarInvestigadores()
        {
            try
            {
                // Solo traemos a investigadores que no son líderes actualmente
                string query = "SELECT idInv, nombre_inv + ' ' + apellido_inv AS nombre_completo FROM INVESTIGADOR WHERE tipo_inv = 'INVESTIGADOR'";
                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBoxInvestigadorLider.DataSource = dt;
                comboBoxInvestigadorLider.DisplayMember = "nombre_completo";
                comboBoxInvestigadorLider.ValueMember = "idInv";

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar investigadores libres: " + ex.Message);
                cn.Cerrar();
            }
        }
        private void btnGuardarSemillero_Click(object sender, EventArgs e)
        {
            AgregarSemilleros();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar? Se perderán los datos ingresados.", "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
    
}
