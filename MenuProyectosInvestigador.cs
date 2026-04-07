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
    public partial class MenuProyectosInvestigador : Form
    {
        int idInvestigadorSesion;
        conexion cn = new conexion();
        public MenuProyectosInvestigador(int idInvestigadorSesion)
        {
            InitializeComponent();
            this.idInvestigadorSesion = idInvestigadorSesion;
        }
        public void ConsultarProyectos()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();

                // el inner join es para traer solo los proyectos asociados al semillero del investigador
                string query = @"SELECT 
                                    P.idPROYECTO AS [ID], 
                                    P.nombre_proyecto AS [Nombre del Proyecto], 
                                    P.FECHA_INICIO AS [Fecha Inicio], 
                                    P.fecha_final_proyecto AS [Fecha Fin],
                                    P.objetivo_proyecto AS [Descripción],
                                    P.estado_proyecto AS [Estado]
                                 FROM PROYECTO P
                                 INNER JOIN INVESTIGADOR I ON P.SEMILLERO_idSEMILLERO = I.SEMILLERO_idSEMILLERO
                                 WHERE I.idInv = @idInv";

                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@idInv", idInvestigadorSesion);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridProyectosInv.DataSource = dt;
                dataGridProyectosInv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                cn.Cerrar();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron proyectos asociados a tu semillero.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar proyectos: " + ex.Message, "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }

        private void btnInicioInvestigador_Click(object sender, EventArgs e)
        {
            menuinicialinvestigador frminicialinvestigador = new menuinicialinvestigador(idInvestigadorSesion);
            frminicialinvestigador.Show();
            this.Hide();
        }

        private void btnMiSemilleroInvestigador_Click(object sender, EventArgs e)
        {
            MenuSemilleroInvestigador frmsemillero = new MenuSemilleroInvestigador(idInvestigadorSesion);
            frmsemillero.Show();
            this.Hide();
        }

        private void btnPerfilInvestigador_Click(object sender, EventArgs e)
        {
            PerfilInvestigador frmPerfilInvestigador = new PerfilInvestigador(idInvestigadorSesion);
            frmPerfilInvestigador.Show();
            this.Hide();
        }

        private void btnSalirInvestigador_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Cerrar Sesión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnConsultarProyectosInvestigador_Click(object sender, EventArgs e)
        {
            ConsultarProyectos();
        }

        private void MenuProyectosInvestigador_Load(object sender, EventArgs e)
        {

        }

        private void dataGridProyectosInv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btndetalles_Click(object sender, EventArgs e)
        {
            if (dataGridProyectosInv.SelectedRows.Count > 0)
            {
                int idProyecto = Convert.ToInt32(dataGridProyectosInv.CurrentRow.Cells[0].Value);
                VerDetallesInv frmDetalles = new VerDetallesInv(idProyecto);
                frmDetalles.ShowDialog(); 
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un proyecto de la lista para ver sus detalles.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
