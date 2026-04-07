using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mock_DSIG
{
    public partial class menuinicialinvestigador : Form
    {
        int idInvestigadorSesion;
        conexion cn = new conexion();
        public menuinicialinvestigador(int idRecibido)
        {
            InitializeComponent();
            this.idInvestigadorSesion = idRecibido;
        }

        private void menuinicialinvestigador_Load(object sender, EventArgs e)
        {
                CargarDatosInv();
        }
        public void CargarDatosInv()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // consulta para el nombre del investigador
                string queryNombre = "SELECT nombre_inv, apellido_inv FROM INVESTIGADOR WHERE idInv = @id";
                SqlCommand cmdNombre = new SqlCommand(queryNombre, conexionAbierta);
                cmdNombre.Parameters.AddWithValue("@id", idInvestigadorSesion);
                SqlDataReader reader = cmdNombre.ExecuteReader();

                if (reader.Read())
                {
                    label10.Text = reader["nombre_inv"].ToString() + " " + reader["apellido_inv"].ToString();
                }
                reader.Close(); // se cierra el reader para ejecutar otras consultas asi no se mezclan los resultados
                // cantidad de proyectos
                string queryProy = "SELECT COUNT(P.idPROYECTO) FROM PROYECTO P INNER JOIN INVESTIGADOR I ON P.SEMILLERO_idSEMILLERO = I.SEMILLERO_idSEMILLERO WHERE I.idInv = @idInv";
                SqlCommand cmd1 = new SqlCommand(queryProy, conexionAbierta);
                cmd1.Parameters.AddWithValue("@idInv", idInvestigadorSesion);
                lblSemillerossActivos.Text = cmd1.ExecuteScalar().ToString();
                // cantidad de compañeros del mismo semillero
                string queryComp = "SELECT COUNT(*) FROM INVESTIGADOR WHERE SEMILLERO_idSEMILLERO = (SELECT SEMILLERO_idSEMILLERO FROM INVESTIGADOR WHERE idInv = @id) AND idInv <> @id";
                SqlCommand cmd2 = new SqlCommand(queryComp, conexionAbierta);
                cmd2.Parameters.AddWithValue("@id", idInvestigadorSesion);
                lblCantComp.Text = cmd2.ExecuteScalar().ToString();
                //cantidad de eventos
                string queryEventos = "SELECT COUNT(*) FROM EVENTO";
                SqlCommand cmd3 = new SqlCommand(queryEventos, conexionAbierta);
                lblCantEvent.Text = cmd3.ExecuteScalar().ToString();
                // cantidad de participaciones
                string queryPart = @"SELECT COUNT(PA.id_participacion) FROM PARTICIPACION PA INNER JOIN PROYECTO_INVESTIGADOR PI ON PA.PROYECTO_idPROYECTO = PI.idPROYECTO WHERE PI.idINVESTIGADOR = @id";
                SqlCommand cmd4 = new SqlCommand(queryPart, conexionAbierta);
                cmd4.Parameters.AddWithValue("@id", idInvestigadorSesion);
                lblParticp.Text = cmd4.ExecuteScalar().ToString();

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
                if (cn.Conectar().State == System.Data.ConnectionState.Open) cn.Cerrar();
            }
        }

        private void btnMiSemilleroInvestigador_Click(object sender, EventArgs e)
        {
            MenuSemilleroInvestigador frmsemillero = new MenuSemilleroInvestigador(idInvestigadorSesion);
            frmsemillero.Show();
            this.Hide();
        }

        private void btnInicioInvestigador_Click(object sender, EventArgs e)
        {

        }

        private void btnProyectosInvestigador_Click(object sender, EventArgs e)
        {
            MenuProyectosInvestigador frmProyectosinvestigador = new MenuProyectosInvestigador(idInvestigadorSesion);
            frmProyectosinvestigador.Show();
            this.Hide();
        }

        private void btnPerfilInvestigador_Click(object sender, EventArgs e)
        {
            PerfilInvestigador frmPerfilInvestigador = new PerfilInvestigador(idInvestigadorSesion);
            frmPerfilInvestigador.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnSalirInvestigador_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Cerrar Sesión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
