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
    public partial class AgregarFaseLider : Form
    {
        conexion cn = new conexion();
        int idProyectoRecibido;
        public AgregarFaseLider(int idProyecto)
        {
            InitializeComponent();
            this.idProyectoRecibido = idProyecto;
        }

        private void btnAGREGARFASE_Click(object sender, EventArgs e)
        {
          
        }

        private void btnCancelarfASE_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar? Se perderán los datos ingresados.", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnAGREGARFASE_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDFASE.Text) || string.IsNullOrEmpty(txtNomFase.Text))
            {
                MessageBox.Show("El Líder debe asignar un ID y un Nombre a la fase.", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // se valida que no exista un ID de fase igual al que se quiere ingresar para evitar duplicados
                string queryValidar = "SELECT COUNT(*) FROM FASES WHERE idFASES = @idBusqueda";
                SqlCommand cmdValidar = new SqlCommand(queryValidar, conexionAbierta);
                cmdValidar.Parameters.AddWithValue("@idBusqueda", txtIDFASE.Text);

                int existe = (int)cmdValidar.ExecuteScalar(); 

                if (existe > 0)
                {
                    MessageBox.Show("El ID de fase '" + txtIDFASE.Text + "' ya existe en el sistema. Por favor, asigne un número diferente.", "ID Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cn.Cerrar();
                    return; 
                }
                string queryInsert = "INSERT INTO FASES (idFASES, PROYECTO_idPROYECTO, nombre_fase, fecha_inicio_fase, descripcion_fase) VALUES (@idFASES, @PROYECTO_idPROYECTO, @nombre_fase, @fecha_inicio_fase, @descripcion_fase)";

                SqlCommand cmdInsert = new SqlCommand(queryInsert, conexionAbierta);
                cmdInsert.Parameters.AddWithValue("@idFASES", Convert.ToInt32(txtIDFASE.Text));
                cmdInsert.Parameters.AddWithValue("@PROYECTO_idPROYECTO", idProyectoRecibido);
                cmdInsert.Parameters.AddWithValue("@nombre_fase", txtNomFase.Text);
                cmdInsert.Parameters.AddWithValue("@fecha_inicio_fase", dateTimeFechaIniFase.Value);
                cmdInsert.Parameters.AddWithValue("@descripcion_fase", txtDescpFase.Text);

                cmdInsert.ExecuteNonQuery();
                cn.Cerrar();

                MessageBox.Show("Fase registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
                cn.Cerrar();
            }
        }
    }
}