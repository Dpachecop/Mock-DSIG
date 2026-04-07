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
    public partial class PantallaReporteLider : Form
    {
        int idLider;
        conexion cn = new conexion();   
        public PantallaReporteLider(int idRecibido)
        {
            InitializeComponent();
            this.idLider = idRecibido;
        }

        private void btnGenerarR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDReporte.Text) || string.IsNullOrEmpty(txtNombreR.Text))
            {
                MessageBox.Show("Por favor complete los campos obligatorios.");
                return;
            }

            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // consulta para obtener el id del semillero del lider logueado
                string querySemillero = "SELECT idSEMILLERO FROM SEMILLERO WHERE idInv = @idLider";
                SqlCommand cmdSem = new SqlCommand(querySemillero, conexionAbierta);
                cmdSem.Parameters.AddWithValue("@idLider", idLider);
                int idSemillero = Convert.ToInt32(cmdSem.ExecuteScalar());
                // consulta para insertar el nuevo reporte con el id del semillero obtenido
                string queryInsert = "INSERT INTO REPORTE (idREPORTE, SEMILLERO_idSEMILLERO, nombre_reporte, descripcion_reporte, tipo_reporte, fecha_reporte)  VALUES (@idREPORTE, @SEMILLERO_idSEMILLERO, @nombre_reporte, @descripcion_reporte, @tipo_reporte, @fecha_reporte)";
                SqlCommand cmd = new SqlCommand(queryInsert, conexionAbierta);
                cmd.Parameters.AddWithValue("@idREPORTE", txtIDReporte.Text);
                cmd.Parameters.AddWithValue("@SEMILLERO_idSEMILLERO", idSemillero);
                cmd.Parameters.AddWithValue("@nombre_reporte", txtNombreR.Text);
                cmd.Parameters.AddWithValue("@descripcion_reporte", txtDescR.Text);
                cmd.Parameters.AddWithValue("@tipo_reporte", txtTipoR.Text);
                cmd.Parameters.AddWithValue("@fecha_reporte", dtPReport.Value);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Reporte generado exitosamente.");
                cn.Cerrar();
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar reporte: " + ex.Message);
                if (cn.Conectar().State == System.Data.ConnectionState.Open) cn.Cerrar();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Está seguro que desea cancelar? Se perderán los datos ingresados.", "DSIG", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close(); 
            }
        }
    }
}
    

