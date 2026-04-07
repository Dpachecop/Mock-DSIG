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

    public partial class MenuSemilleroInvestigador : Form
    {
        int idInvestigadorSesion;
        conexion cn = new conexion();
        public MenuSemilleroInvestigador(int idInvestigadorSesion)
        {
            InitializeComponent();
            this.idInvestigadorSesion = idInvestigadorSesion;
        }

        private void MenuSemilleroInvestigador_Load(object sender, EventArgs e)
        {
            CBXestadoInvestigador.Enabled = false;
            dTPFecharInvestigador.Enabled = false;
            CargarSemillero();
            CargarCompañeros();
        }
        public void CargarSemillero()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // consulta para obtener el semillero del investigador logueado
                string query = "SELECT S.nombre_semillero, S.area_conocimiento, S.estado_semillero, S.fecha_creacion FROM SEMILLERO S INNER JOIN INVESTIGADOR I ON S.idSEMILLERO = I.SEMILLERO_idSEMILLERO WHERE I.idInv = @idInv";
                SqlCommand cmd = new SqlCommand(query, conexionAbierta); 
                cmd.Parameters.AddWithValue("@idInv", idInvestigadorSesion);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read()) // si los datos leidos son correctos se asignan a los campos correspondientes
                {
                    string nombreSemillero = dr["nombre_semillero"].ToString();
                    txtNombre.Text = nombreSemillero;
                    label10.Text = nombreSemillero;
                    txtAreadeconocimiento.Text = dr["area_conocimiento"].ToString();
                    CBXestadoInvestigador.Text = dr["estado_semillero"].ToString();
                    dTPFecharInvestigador.Value = Convert.ToDateTime(dr["fecha_creacion"]);
                }
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }
        public void CargarCompañeros()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                string query = "SELECT nombre_inv AS [Nombre], apellido_inv AS [Apellido], correo_inv AS [Correo], tipo_inv AS [Rol] FROM INVESTIGADOR WHERE SEMILLERO_idSEMILLERO = (SELECT SEMILLERO_idSEMILLERO FROM INVESTIGADOR WHERE idInv = @idInv) AND idInv <> @idInv";
                SqlDataAdapter da = new SqlDataAdapter(query, conexionAbierta);
                da.SelectCommand.Parameters.AddWithValue("@idInv", idInvestigadorSesion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtcompas.DataSource = dt;
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        }

        private void btnProyectosInvestigador_Click(object sender, EventArgs e)
        {
            MenuProyectosInvestigador frmProyectosinvestigador = new MenuProyectosInvestigador(idInvestigadorSesion);
            frmProyectosinvestigador.Show();
            this.Hide();
        }

        private void btnPertfilInvestigador_Click(object sender, EventArgs e)
        {
            PerfilInvestigador frmPerfilInvestigador = new PerfilInvestigador(idInvestigadorSesion);
            frmPerfilInvestigador.Show();
            this.Hide();
        }

        private void btnSalirInvestigador_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmar cierre de sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
