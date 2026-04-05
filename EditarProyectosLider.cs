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
    public partial class EditarProyectosLider : Form
    {
        conexion cn = new conexion();
        int idProyectoAEditar;
        public EditarProyectosLider(int idProyecto)
        {
            InitializeComponent();
            this.idProyectoAEditar = idProyecto;
        }

        private void EditarProyectosLider_Load(object sender, EventArgs e)
        {
            CargarDatosProyecto();
        }
        public void CargarDatosProyecto()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // Traemos todos los campos que tienes en el diseño
                string query = @"SELECT idPROYECTO, nombre_proyecto, FECHA_INICIO, fecha_final_proyecto, estado_proyecto, objetivo_proyecto FROM PROYECTO WHERE idPROYECTO = @id";
                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@id", idProyectoAEditar);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Asignamos valores a tus controles específicos
                    txtidPROYECTOeditLIDER.Text = reader["idPROYECTO"].ToString();
                    txtNombreProyectoeditLIDER.Text = reader["nombre_proyecto"].ToString();
                    dateTimePickerInicioedit.Value = Convert.ToDateTime(reader["FECHA_INICIO"]);

                    // Manejo de fecha final (por si es nula en la BD)
                    if (reader["fecha_final_proyecto"] != DBNull.Value)
                        dateTimePickerFinaledit.Value = Convert.ToDateTime(reader["fecha_final_proyecto"]);

                    comboBoxEstadoProyectoeditLIDER.Text = reader["estado_proyecto"].ToString();
                    txtDescripcionProyectoeditLIDER.Text = reader["objetivo_proyecto"].ToString();
                }
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void btnActualizarInfoLIDER_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtNombreProyectoeditLIDER.Text))
            {
                MessageBox.Show("El nombre del proyecto es obligatorio.");
                return;
            }

            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                string query = "UPDATE PROYECTO SET nombre_proyecto = @nombre_proyecto, FECHA_INICIO = @FECHA_INICIO, fecha_final_proyecto = @fecha_final_proyecto, estado_proyecto = @estado_proyecto, objetivo_proyecto = @objetivo_proyecto WHERE idPROYECTO = @id";

                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@nombre_proyecto", txtNombreProyectoeditLIDER.Text);
                cmd.Parameters.AddWithValue("@FECHA_INICIO", dateTimePickerInicioedit.Value);
                cmd.Parameters.AddWithValue("@fecha_final_proyecto", dateTimePickerFinaledit.Value);
                cmd.Parameters.AddWithValue("@estado_proyecto", comboBoxEstadoProyectoeditLIDER.Text);
                cmd.Parameters.AddWithValue("@objetivo_proyecto", txtDescripcionProyectoeditLIDER.Text);
                cmd.Parameters.AddWithValue("@id", idProyectoAEditar);

                int resultado = cmd.ExecuteNonQuery();
                cn.Cerrar();

                if (resultado > 0)
                {
                    MessageBox.Show("Proyecto actualizado con éxito.", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void btnCancelarEditLIDER_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar? Se perderán los cambios realizados.", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }

}
