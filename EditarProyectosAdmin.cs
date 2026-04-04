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
    public partial class EditarProyectosAdmin : Form
    {
        // Instancia de la clase conexión
        conexion cn = new conexion();
        int idProyectoRecibido;

        public EditarProyectosAdmin(int id)
        {
            InitializeComponent();
            this.idProyectoRecibido = id;

            // Cargamos primero la lista de semilleros y luego los datos del proyecto
            CargarSemilleros();
            CargarDatosProyecto();
        }

        private void CargarSemilleros()
        {
            try
            {
                string query = "SELECT idSEMILLERO, nombre_semillero FROM SEMILLERO";
                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBoxSemillerosedit.DataSource = dt;
                comboBoxSemillerosedit.DisplayMember = "nombre_semillero";
                comboBoxSemillerosedit.ValueMember = "idSEMILLERO";

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de semilleros: " + ex.Message);
                cn.Cerrar();
            }
        }
        public void CargarDatosProyecto()
        {
            // Bloqueamos el ID para que no sea editable
            txtidPROYECTOedit.ReadOnly = true;

            try
            {
                string query = @"SELECT * FROM PROYECTO WHERE idPROYECTO = @id";

                SqlCommand cmd = new SqlCommand(query, cn.Conectar());
                cmd.Parameters.AddWithValue("@id", idProyectoRecibido);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtidPROYECTOedit.Text = dr["idPROYECTO"].ToString();
                    txtNombreProyectoedit.Text = dr["nombre_proyecto"].ToString();
                    txtDescripcionProyectoedit.Text = dr["objetivo_proyecto"].ToString();

                    if (dr["FECHA_INICIO"] != DBNull.Value)
                    {
                        dateTimePickerInicioedit.Value = Convert.ToDateTime(dr["FECHA_INICIO"]);
                    }
                    if (dr["fecha_final_proyecto"] != DBNull.Value)
                    {
                        dateTimePickerFinaledit.Value = Convert.ToDateTime(dr["fecha_final_proyecto"]);
                    }
                    if (dr["SEMILLERO_idSEMILLERO"] != DBNull.Value)
                    {
                        comboBoxSemillerosedit.SelectedValue = dr["SEMILLERO_idSEMILLERO"];
                    }
                    if (dr["estado_proyecto"] != DBNull.Value)
                    {
                        comboBoxEstadoProyectoedit.SelectedItem = dr["estado_proyecto"].ToString();
                    }
                        
                }
                dr.Close();
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del proyecto: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void btnActualizarInfo_Click(object sender, EventArgs e)
        {
            try
            {
                string queryProyecto = @"UPDATE PROYECTO SET nombre_proyecto = @nombre, objetivo_proyecto = @desc,  FECHA_INICIO = @inicio, fecha_final_proyecto = @final, estado_proyecto = @estado, SEMILLERO_idSEMILLERO = @idSemi WHERE idPROYECTO = @id";
                SqlCommand cmdP = new SqlCommand(queryProyecto, cn.Conectar());
                cmdP.Parameters.AddWithValue("@nombre", txtNombreProyectoedit.Text);
                cmdP.Parameters.AddWithValue("@desc", txtDescripcionProyectoedit.Text);
                cmdP.Parameters.AddWithValue("@inicio", dateTimePickerInicioedit.Value);
                cmdP.Parameters.AddWithValue("@final", dateTimePickerFinaledit.Value);
                cmdP.Parameters.AddWithValue("@id", idProyectoRecibido);
                cmdP.Parameters.AddWithValue("@idSemi", comboBoxSemillerosedit.SelectedValue);

                // Validación simple para el estado
                string estado = comboBoxEstadoProyectoedit.SelectedItem != null ? comboBoxEstadoProyectoedit.SelectedItem.ToString() : "Activo";
                cmdP.Parameters.AddWithValue("@estado", estado);
                cmdP.ExecuteNonQuery();
                MessageBox.Show("Proyecto actualizado correctamente.", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.Cerrar();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los cambios: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void btnCancelarEdit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}