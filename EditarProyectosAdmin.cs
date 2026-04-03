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

        // Constructor que recibe el ID seleccionado en el DataGrid
        public EditarProyectosAdmin(int id)
        {
            InitializeComponent();
            this.idProyectoRecibido = id;

            // El orden es vital: primero las listas, luego los datos del proyecto
            CargarSemilleros();
            CargarInvestigadores();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de semilleros: " + ex.Message);
            }
        }

        private void CargarInvestigadores()
        {
            try
            {
                string query = "SELECT idInv, nombre_inv + ' ' + apellido_inv AS nombre_completo FROM INVESTIGADOR";
                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBoxLideredit.DataSource = dt;
                comboBoxLideredit.DisplayMember = "nombre_completo";
                comboBoxLideredit.ValueMember = "idInv";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de investigadores: " + ex.Message);
            }
        }

        public void CargarDatosProyecto() 
        {
            // Bloqueamos el ID para que no sea editable
            txtidPROYECTOedit.ReadOnly = true;

            try
            {
                string query = @"SELECT P.*, PI.idINVESTIGADOR 
                                FROM PROYECTO P 
                                LEFT JOIN PROYECTO_INVESTIGADOR PI ON P.idPROYECTO = PI.idPROYECTO 
                                WHERE P.idPROYECTO = @id";

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
                        comboBoxSemillerosedit.Enabled = false; 
                    }

                    // Cargar Investigador Líder actual
                    if (dr["idINVESTIGADOR"] != DBNull.Value)
                    {
                        comboBoxLideredit.SelectedValue = dr["idINVESTIGADOR"];
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del proyecto: " + ex.Message);
            }
        }

        private void btnActualizarInfo_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                string queryProyecto = @"UPDATE PROYECTO SET 
                                        nombre_proyecto = @nombre, 
                                        objetivo_proyecto = @desc, 
                                        FECHA_INICIO = @inicio, 
                                        fecha_final_proyecto = @final,
                                        estado_proyecto = @estado
                                        WHERE idPROYECTO = @id";

                SqlCommand cmdP = new SqlCommand(queryProyecto, conexionAbierta);
                cmdP.Parameters.AddWithValue("@nombre", txtNombreProyectoedit.Text);
                cmdP.Parameters.AddWithValue("@desc", txtDescripcionProyectoedit.Text);
                cmdP.Parameters.AddWithValue("@inicio", dateTimePickerInicioedit.Value);
                cmdP.Parameters.AddWithValue("@final", dateTimePickerFinaledit.Value);
                cmdP.Parameters.AddWithValue("@id", idProyectoRecibido);
                cmdP.Parameters.AddWithValue("@estado", comboBoxEstadoProyectoedit.SelectedItem.ToString());
                cmdP.ExecuteNonQuery();

                // Borra al lider anterior e Insertar nuevo (si el usuario lo desea, si no queda igual)
                string queryPuente = @"DELETE FROM PROYECTO_INVESTIGADOR WHERE idPROYECTO = @id;
                                      INSERT INTO PROYECTO_INVESTIGADOR (idPROYECTO, idINVESTIGADOR) 
                                      VALUES (@id, @idInv)";

                SqlCommand cmdI = new SqlCommand(queryPuente, conexionAbierta);
                cmdI.Parameters.AddWithValue("@id", idProyectoRecibido);
                cmdI.Parameters.AddWithValue("@idInv", comboBoxLideredit.SelectedValue);
                cmdI.ExecuteNonQuery();

                MessageBox.Show("Proyecto actualizado correctamente.", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los cambios: " + ex.Message);
            }
        }

        private void btnCancelarEdit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}