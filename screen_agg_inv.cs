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
    public partial class screen_agg_inv : Form
    {
        public screen_agg_inv()
        {
            InitializeComponent();
        }

        conexion cn = new conexion(); // Instancia de la clase Conexion para establecer la conexión con la base de datos    

        public void Insertar_Usuario(int idInv, String nombre_inv, String apellido_inv, String correo_inv, String numero_tel_inv,  String contrasenia_inv, string tipo_inv)
        {
            SqlCommand Insert;

            try
            {
                Insert = new SqlCommand("INSERT INTO INVESTIGADOR (idInv, nombre_inv, apellido_inv,correo_inv,  numero_tel_inv, contrasenia_inv, tipo_inv ) VALUES (@idInv, @nombre_inv, @apellido_inv, @correo_inv,@numero_tel_inv, @contrasenia_inv,@tipo_inv)", cn.Conectar()); // Comando SQL para ejecutar la consulta
                Insert.CommandType = CommandType.Text; // Consulta SQL de tipo TEXTO
                Insert.Parameters.AddWithValue("@idInv", SqlDbType.Int).Value = idInv;
                Insert.Parameters.AddWithValue("@nombre_inv", SqlDbType.NVarChar).Value = nombre_inv; // 
                Insert.Parameters.AddWithValue("@apellido_inv", SqlDbType.NVarChar).Value = apellido_inv;
                Insert.Parameters.AddWithValue("@correo_inv", SqlDbType.NVarChar).Value = correo_inv;
                Insert.Parameters.AddWithValue("@numero_tel_inv", SqlDbType.NVarChar).Value = numero_tel_inv; // 
                Insert.Parameters.AddWithValue("@contrasenia_inv", SqlDbType.NVarChar).Value = contrasenia_inv;
                Insert.Parameters.AddWithValue("@tipo_inv", SqlDbType.NVarChar).Value = tipo_inv;

                Insert.ExecuteNonQuery();

                MessageBox.Show("Registro Exitoso de el usuario, puede revisarlo.", "INSERCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("El Usuario Ya Existe" + "\n" + "Verifique los Datos", "INSERCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bt_guardar_Click(object sender, EventArgs e)
        {
            if  (form_correo.Text == "" || form_password_inic.Text == "" || form_nombres.Text == "" || form_apellido.Text == "" || form_numero_tlf.Text == "" || form_id.Text == "" || combo_rol.SelectedIndex == -1 || combo_tipo_doc.Text == "")
            {
                MessageBox.Show("Debe Ingresar Datos Validos para Poder Registrar el Usuario", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (form_password_inic.Text != form_password_final.Text)
                {
                    MessageBox.Show("Las Contraseñas No Coinciden", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    form_password_inic.Clear();
                    form_password_final.Clear();
                    form_password_inic.Focus();
                    return;
                }


                Insertar_Usuario(int.Parse(form_id.Text), form_nombres.Text, form_apellido.Text, form_correo.Text, form_numero_tlf.Text, form_password_final.Text, combo_rol.SelectedItem.ToString());
                this.Hide();
            }
        }
    }
}
