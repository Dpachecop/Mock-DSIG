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
    public partial class Form2 : Form
    {
        conexion cn = new conexion();
        public Form2()
        {
            InitializeComponent();
        }

        private void btnInicioAdmin_Click(object sender, EventArgs e)
        {
            MenuInicialAdministrador menuInicialAdministrador = new MenuInicialAdministrador();
            menuInicialAdministrador.Show();
            this.Hide();
        }

        private void btnSemillerosAdmin2_Click(object sender, EventArgs e)
        {

        }

        private void btnProyectosAdmin_Click(object sender, EventArgs e)
        {
            Form3 frmProyectos = new Form3();
            frmProyectos.Show();
            this.Hide();
        }

        private void btnUsuariosAdmin_Click(object sender, EventArgs e)
        {
            Form4 frmUsuariosAdmin = new Form4(); 
            frmUsuariosAdmin.Show();
            this.Hide();
        }

        private void btnReportesAdmin_Click(object sender, EventArgs e)
        {
            Form5 frmReportes = new Form5();
            frmReportes.Show();
            this.Hide();
        }
        public void EliminarSemilleros()
        {
            if (dataGridSemilleros.SelectedRows.Count > 0)
            {
                try
                {
                    int idSemillero = 0; // Valor por defecto en caso de que no se pueda obtener el ID
                    string nombreSem = "Semillero"; // Valor por defecto para el nombre del semillero
                    if (dataGridSemilleros.CurrentRow.Cells.Count > 0)
                    {
                        idSemillero = Convert.ToInt32(dataGridSemilleros.CurrentRow.Cells[0].Value); // Se asume que la primera columna es el ID

                        if (dataGridSemilleros.CurrentRow.Cells.Count > 1) // Se verifica que exista la segunda columna para obtener el nombre del semillero
                        {
                            nombreSem = dataGridSemilleros.CurrentRow.Cells[1].Value.ToString(); // Se asume que la segunda columna es el nombre del semillero
                        }
                    } // esto es porque en caso de que el formato del datagrid cambie o no se pueda obtener el ID o el nombre, se asignan valores por defecto para evitar errores y aun asi mostrar una confirmación al usuario

                    DialogResult confirmacion = MessageBox.Show(
                        $"¿Está seguro de eliminar el semillero '{nombreSem}'? \nID: {idSemillero}",
                        "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmacion == DialogResult.Yes)
                    {
                        SqlConnection conexionAbierta = cn.Conectar();

                       
                        string queryLider = "SELECT idInv FROM SEMILLERO WHERE idSEMILLERO = @id";
                        SqlCommand cmdLider = new SqlCommand(queryLider, conexionAbierta);
                        cmdLider.Parameters.AddWithValue("@id", idSemillero);
                        object result = cmdLider.ExecuteScalar();

                        if (result != null)
                        {
                            // se elimina el semillero y se actualiza el rol del investigador a 'INVESTIGADOR' 
                            int idInvestigador = Convert.ToInt32(result);
                            string queryDelete = "DELETE FROM SEMILLERO WHERE idSEMILLERO = @id";
                            SqlCommand cmdDel = new SqlCommand(queryDelete, conexionAbierta);
                            cmdDel.Parameters.AddWithValue("@id", idSemillero);
                            cmdDel.ExecuteNonQuery();
                            // entonces aca ya el lider del semillero anterior pasa a investigador 
                            string queryUpdate = "UPDATE INVESTIGADOR SET tipo_inv = 'INVESTIGADOR' WHERE idInv = @idInv";
                            SqlCommand cmdUpd = new SqlCommand(queryUpdate, conexionAbierta);
                            cmdUpd.Parameters.AddWithValue("@idInv", idInvestigador);
                            cmdUpd.ExecuteNonQuery();

                            MessageBox.Show("Eliminado con éxito.");
                        }

                        cn.Cerrar();
                        ConsultarSemilleros(); 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar la fila: " + ex.Message);
                    cn.Cerrar();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa (clic en la flecha de la izquierda).");
            }
        }
        public void ConsultarSemilleros()
        {
            try
            {
                DataTable dt = new DataTable();
                // Une Semillero con Investigador y cuenta los proyectos asociados
                string query = @"SELECT S.idSEMILLERO AS [ID], S.nombre_semillero AS [Semillero], S.area_conocimiento AS [Área], I.nombre_inv + ' ' + I.apellido_inv AS [Líder Responsable], S.estado_semillero AS [Estado], S.fecha_creacion AS [Fecha de Creación], COUNT(P.idPROYECTO) AS [Proyectos Activos]
                                 FROM SEMILLERO S
                                 INNER JOIN INVESTIGADOR I ON S.idInv = I.idInv
                                 LEFT JOIN PROYECTO P ON S.idSEMILLERO = P.SEMILLERO_idSEMILLERO
                                 GROUP BY 
                                    S.idSEMILLERO, S.nombre_semillero, S.area_conocimiento, 
                                    I.nombre_inv, I.apellido_inv, S.estado_semillero, S.fecha_creacion";

                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                da.Fill(dt);

                // Limpieza y carga del Grid
                dataGridSemilleros.DataSource = null;
                dataGridSemilleros.Columns.Clear();
                dataGridSemilleros.DataSource = dt;
                dataGridSemilleros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                cn.Cerrar();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron semilleros registrados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar semilleros: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void btnConsultarSemillerosAdmin_Click(object sender, EventArgs e)
        {
            ConsultarSemilleros();
        }

        private void btnEditSemilleros_Click(object sender, EventArgs e)
        {
      
        }

        private void btnAggSemilleros_Click(object sender, EventArgs e)
        {
            AgregarSemilleroAdmin AgregarSemilleros = new AgregarSemilleroAdmin();
            AgregarSemilleros.Show();
        }

        private void btnEliminarSemilleros_Click(object sender, EventArgs e)
        {
            EliminarSemilleros();
        }
    }
}
