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
    public partial class Form3 : Form
    {
        conexion cn = new conexion();
        DataSet ds = new DataSet();
        public Form3()
        {
            InitializeComponent();
        }
        public void ConsultarDatosProyectosAdmin()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"SELECT 
                            P.idPROYECTO AS [ID], 
                            P.nombre_proyecto AS [Proyecto], 
                            S.nombre_semillero AS [Semillero], 
                            I.nombre_inv + ' ' + I.apellido_inv AS [Lider],
                            P.FECHA_INICIO AS [Inicio], 
                            P.fecha_final_proyecto AS [Fin],
                            P.objetivo_proyecto AS [Objetivo],
                            P.estado_proyecto AS [Estado]
                         FROM PROYECTO P
                         INNER JOIN SEMILLERO S ON P.SEMILLERO_idSEMILLERO = S.idSEMILLERO
                         LEFT JOIN PROYECTO_INVESTIGADOR PI ON P.idPROYECTO = PI.idPROYECTO
                         LEFT JOIN INVESTIGADOR I ON PI.idINVESTIGADOR = I.idInv";
                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                da.Fill(dt);

                dataGridProyectos.DataSource = null;
                dataGridProyectos.Columns.Clear();
                dataGridProyectos.DataSource = dt;
                dataGridProyectos.Refresh(); // Refresca y actualiza el datagrid por si se crean celdas vacias o si se agregan nuevos proyectos 

                cn.Cerrar();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("La consulta no devolvió filas. Verifica que existan datos en la tabla PROYECTO.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void btnSemillerosAdmin_Click(object sender, EventArgs e)
        {
            Form2 frmSemillerosAdmin = new Form2();
            frmSemillerosAdmin.Show();
            this.Hide();
        }

        private void btnInicioAdmin_Click(object sender, EventArgs e)
        {
            MenuInicialAdministrador menuInicialAdministrador = new MenuInicialAdministrador();
            menuInicialAdministrador.Show();
            this.Hide();
        }

        private void btnUsuariosAdmin_Click(object sender, EventArgs e)
        {
            Form4 frmUsuarios = new Form4();
            frmUsuarios.Show();
            this.Hide();
        }

        private void btnReportesAdmin_Click(object sender, EventArgs e)
        {
            Form5 frmReportes = new Form5();
            frmReportes.Show();
            this.Hide();
        }

        private void btnAggProyectos_Click(object sender, EventArgs e)
        {
            Form6 frmRegistroProyecto = new Form6();
            frmRegistroProyecto.ShowDialog();
        }

        private void btnConsultarProyectosAdmin_Click(object sender, EventArgs e)
        {
            ConsultarDatosProyectosAdmin();
        }

        private void btnSalirAdmin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que deseas salir?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnEditProyectos_Click(object sender, EventArgs e)
        {
            // Validar que haya una fila seleccionada
            if (dataGridProyectos.SelectedRows.Count > 0)
            {
                int idProyecto = Convert.ToInt32(dataGridProyectos.CurrentRow.Cells["ID"].Value);
                EditarProyectosAdmin frmEditar = new EditarProyectosAdmin(idProyecto);
                frmEditar.ShowDialog();
                ConsultarDatosProyectosAdmin();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila del listado para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void EliminarProyectos()
        {
            if (dataGridProyectos.SelectedRows.Count > 0)
            {
                int idProyecto = Convert.ToInt32(dataGridProyectos.CurrentRow.Cells["ID"].Value);

                if (MessageBox.Show("¿Desea eliminar este proyecto y sus vínculos?", "ELIMINAR PROYECTOS",MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        SqlConnection conexionAbierta = cn.Conectar();

                        // EL FLUJO EN UNA SOLA CONSULTA:
                        string query = @"DELETE FROM PROYECTO_INVESTIGADOR WHERE idPROYECTO = @id;
                        DELETE FROM PROYECTO WHERE idPROYECTO = @id;";
                        // se hace en este orden porque existen relaciones entre las tablas, si se intenta eliminar el proyecto primero no se podrá por la relación con las otras tablas
                        SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                        cmd.Parameters.AddWithValue("@id", idProyecto);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Eliminado con éxito");
                        ConsultarDatosProyectosAdmin(); // Refrescar tabla
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una fila primero.");
                }
            }
        }

        private void btnEliminarProyectos_Click(object sender, EventArgs e)
        {
            EliminarProyectos();
        }
    }
}
