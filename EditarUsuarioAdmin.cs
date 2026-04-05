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
    public partial class EditarUsuarioAdmin : Form
    {
        conexion cn = new conexion();
        int idOriginal; // Para saber a quién editar en el WHERE

        public EditarUsuarioAdmin(int id, string nombre, string apellido, string correo, string telefono, string pass)
        {
            InitializeComponent();
            this.idOriginal = id; // guardamos el id original para usarlo en la consulta UPDATE

            // Llenamos los campos del diseño
            form_idEdit.Text = id.ToString();
            form_nombresEdit.Text = nombre;
            form_apellidoEdit.Text = apellido;
            form_correoEdit.Text = correo;
            form_numero_tlfEdit.Text = telefono;
            form_password_inic.Text = pass;

        }

        private void bt_guardarEdit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                string query = "UPDATE INVESTIGADOR SET  nombre_inv = @nombre_inv, apellido_inv = @apellido_inv,  correo_inv = @correo_inv,  numero_tel_inv = @numero_tel_inv, contrasenia_inv = @contrasenia_inv  WHERE idInv = @id";

                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@nombre_inv", form_nombresEdit.Text);
                cmd.Parameters.AddWithValue("@apellido_inv", form_apellidoEdit.Text);
                cmd.Parameters.AddWithValue("@correo_inv", form_correoEdit.Text);
                cmd.Parameters.AddWithValue("@numero_tel_inv", form_numero_tlfEdit.Text);
                cmd.Parameters.AddWithValue("@contrasenia_inv", form_password_inic.Text);
                cmd.Parameters.AddWithValue("@id", idOriginal);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario actualizado correctamente.");
                cn.Cerrar();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
                cn.Cerrar();
            }
        }
    }
    
}
