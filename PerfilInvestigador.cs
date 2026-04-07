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
    public partial class PerfilInvestigador : Form
    {
        int idInvestigadorSesion;
        conexion cn = new conexion();

        public PerfilInvestigador(int idInvestigadorSesion)
        {
            InitializeComponent();
            this.idInvestigadorSesion = idInvestigadorSesion;
        }
        private void EstadoCampos(bool editable)
        {
            // el !editable significa que si editable es true los campos serán editables y si es false serán de solo lectura
            txtName.ReadOnly = !editable;
            txtCorreo.ReadOnly = !editable;
            txtNumTel.ReadOnly = !editable;
            txtcontra.ReadOnly = !editable;
            txtID.ReadOnly = true;
            txtSemillero.ReadOnly = true;
            txtRol.ReadOnly = true;
        }

        public void CargarDatosPerfil()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // consulta con JOIN para traer el nombre del semillero
                string query = @"SELECT I.*, S.nombre_semillero 
                                 FROM INVESTIGADOR I 
                                 INNER JOIN SEMILLERO S ON I.SEMILLERO_idSEMILLERO = S.idSEMILLERO 
                                 WHERE I.idInv = @id";

                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@id", idInvestigadorSesion);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtName.Text = $"{reader["nombre_inv"]} {reader["apellido_inv"]}";
                    txtCorreo.Text = reader["correo_inv"].ToString();
                    txtID.Text = reader["idInv"].ToString();
                    txtNumTel.Text = reader["numero_tel_inv"].ToString();
                    txtSemillero.Text = reader["nombre_semillero"].ToString();
                    txtRol.Text = reader["tipo_inv"].ToString();
                    txtcontra.Text = reader["contrasenia_inv"].ToString();
                }
                reader.Close();
                cn.Cerrar();

                // bloqueamos campos al cargar
                EstadoCampos(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar perfil: " + ex.Message, "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarDatosInvestigador_Click(object sender, EventArgs e)
        {
            if (btnEditarDatosInvestigador.Text == "EDITAR DATOS")
            {
                EstadoCampos(true);
                btnEditarDatosInvestigador.Text = "GUARDAR";
                btnEditarDatosInvestigador.BackColor = Color.FromArgb(0, 192, 0); 
            }
            else
            {
              
                ActualizarPerfil();
            }
        }

        private void ActualizarPerfil()
        {
            try
            {
                string nombreCompleto = txtName.Text.Trim();
                string nombre = "";
                string apellido = "";

                int primerEspacio = nombreCompleto.IndexOf(' ');
                if (primerEspacio > 0)
                {
                    nombre = nombreCompleto.Substring(0, primerEspacio);
                    apellido = nombreCompleto.Substring(primerEspacio + 1);
                }
                else
                {
                    nombre = nombreCompleto;
                }

                SqlConnection conexionAbierta = cn.Conectar();
                string query = "UPDATE INVESTIGADOR SET nombre_inv = @nombre_inv, apellido_inv = @apellido_inv, correo_inv = @correo_inv, numero_tel_inv = @numero_tel_inv, contrasenia_inv = @contrasenia_inv WHERE idInv = @id";

                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@nombre_inv", nombre);
                cmd.Parameters.AddWithValue("@apellido_inv", apellido);
                cmd.Parameters.AddWithValue("@correo_inv", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@numero_tel_inv", txtNumTel.Text);
                cmd.Parameters.AddWithValue("@contrasenia_inv", txtcontra.Text);
                cmd.Parameters.AddWithValue("@id", idInvestigadorSesion);

                cmd.ExecuteNonQuery();
                cn.Cerrar();

                MessageBox.Show("Perfil actualizado con éxito.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EstadoCampos(false);
                btnEditarDatosInvestigador.Text = "EDITAR DATOS";
                btnEditarDatosInvestigador.BackColor = Color.FromArgb(60, 122, 74); // Volver al color original
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }

        private void PerfilInvestigador_Load(object sender, EventArgs e)
        {
            CargarDatosPerfil();
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

        private void btnProyectosInvestigador_Click(object sender, EventArgs e)
        {
            MenuProyectosInvestigador frmProyectosinvestigador = new MenuProyectosInvestigador(idInvestigadorSesion);
            frmProyectosinvestigador.Show();
            this.Hide();
        }

        private void btnSalirInvestigador_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnCancelarDatosInvestigador_Click(object sender, EventArgs e)
        {
            CargarDatosPerfil();
            btnEditarDatosInvestigador.Text = "EDITAR DATOS";
            MessageBox.Show("Edición cancelada, No se realizarón cambios.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}