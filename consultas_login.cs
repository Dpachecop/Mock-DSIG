using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mock_DSIG
{


    internal class consultas_login
    {

        conexion cn = new conexion(); // Instancia de la clase Conexion para establecer la conexión con la base de datos
        DataSet ds = new DataSet(); // Variable de tipo DataSet para almacenar los datos obtenidos de la base de datos
        Boolean estado_coneccion = false; // Variable booleana para verificar el estado de la conexión

        public Boolean IniciarSesionComoAdmin(String correo_admin, string contrasenia_admin)
        {
            SqlCommand Consulta; // Variable de tipo SqlCommand para ejecutar la consulta SQL
            Consulta = new SqlCommand("select  correo_admin, contrasenia_admin from ADMINISTRADOR where correo_admin = @correo_admin and contrasenia_admin = @contrasenia_admin", cn.Conectar()); // Consulta SQL para verificar el inicio de sesión
            Consulta.CommandType = CommandType.Text; // Tipo de comando SQL
            Consulta.Parameters.AddWithValue("@correo_admin", correo_admin); // Agregar el parámetro correo admin a la consulta SQL
            Consulta.Parameters.AddWithValue("@contrasenia_admin", contrasenia_admin); // Agregar el parámetro PasswordUsuario a la consulta SQL
            Consulta.ExecuteNonQuery(); // Ejecutar la consulta SQL

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Consulta); // Variable de tipo SqlDataAdapter para llenar el DataSet con los datos obtenidos de la consulta SQL
                da.Fill(ds, "ADMINISTRADOR"); // Llenar el DataSet con los datos obtenidos de la consulta SQL
                DataRow dr; // Variable de tipo DataRow para almacenar una fila del DataSet
                dr = ds.Tables["ADMINISTRADOR"].Rows[0]; // Almacenar la primera fila del DataSet en la variable dr
                if (Convert.ToString(correo_admin) == dr["correo_admin"].ToString() && contrasenia_admin == dr["contrasenia_admin"].ToString()) // Verificar si el correo y contrasenia coinciden con los datos obtenidos de la consulta SQL
                {
                    MessageBox.Show("Bienvenido Administrador"); // Si coinciden, mostrar un mensaje de bienvenida para el administrador
                   Form4 formAdministrador = new Form4(); // Crear una instancia del formulario de administrador
                    formAdministrador.Show(); // Mostrar el formulario de administrador
                    estado_coneccion = true; // Si coinciden, establecer el estado de la conexión como verdadero
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario o Contraseña Incorrecta", "FORMULARIO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            return estado_coneccion; // Retornar el estado de la conexión
        }


        public Boolean IniciarSesionComoInvestigador(String correo_inv, string contrasenia_inv)
        {
            SqlCommand Consulta; // Variable de tipo SqlCommand para ejecutar la consulta SQL
            Consulta = new SqlCommand("select  correo_inv, contrasenia_inv, tipo_inv  from INVESTIGADOR where correo_inv = @correo_inv and contrasenia_inv = @contrasenia_inv", cn.Conectar()); // Consulta SQL para verificar el inicio de sesión
            Consulta.CommandType = CommandType.Text; // Tipo de comando SQL
            Consulta.Parameters.AddWithValue("@correo_inv", correo_inv); // Agregar el parámetro correo admin a la consulta SQL
            Consulta.Parameters.AddWithValue("@contrasenia_inv", contrasenia_inv); // Agregar el parámetro PasswordUsuario a la consulta SQL
            Consulta.ExecuteNonQuery(); // Ejecutar la consulta SQL

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Consulta); // Variable de tipo SqlDataAdapter para llenar el DataSet con los datos obtenidos de la consulta SQL
                da.Fill(ds, "INVESTIGADOR"); // Llenar el DataSet con los datos obtenidos de la consulta SQL
                DataRow dr; // Variable de tipo DataRow para almacenar una fila del DataSet
                dr = ds.Tables["INVESTIGADOR"].Rows[0]; // Almacenar la primera fila del DataSet en la variable dr
                if (Convert.ToString(correo_inv) == dr["correo_inv"].ToString() && contrasenia_inv == dr["contrasenia_inv"].ToString() && "LIDER" == dr["tipo_inv"].ToString()) // Verificar si el correo y contrasenia coinciden con los datos obtenidos de la consulta SQL
                {
                    MessageBox.Show("Bienvenido LIDER"); // Si coinciden, mostrar un mensaje de bienvenida para el administrador
                    Form4 formAdministrador = new Form4(); // Crear una instancia del formulario de administrador
                    formAdministrador.Show(); // Mostrar el formulario de administrador
                    estado_coneccion = true; // Si coinciden, establecer el estado de la conexión como verdadero
                }
                else
                {
                    if (Convert.ToString(correo_inv) == dr["correo_inv"].ToString() && contrasenia_inv == dr["contrasenia_inv"].ToString() && "INVESTIGADOR" == dr["tipo_inv"].ToString()) // Verificar si el IDusuario y PasswordUsuario coinciden con los datos obtenidos de la consulta SQL
                    {
                        MessageBox.Show("Bienvenido INVESTIGADOR"); // Si coinciden, mostrar un mensaje de bienvenida para el usuario
                        Form4 formUsuario = new Form4(); // Crear una instancia del formulario de usuario
                        formUsuario.Show(); // Mostrar el formulario de usuario
                        estado_coneccion = true; // Si coinciden, establecer el estado de la conexión como verdadero

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario o Contraseña Incorrecta", "FORMULARIO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            return estado_coneccion; // Retornar el estado de la conexión
        }

    }
}
