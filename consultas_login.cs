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
        int idLiderSesion; // Variable para almacenar el ID del líder que inició sesión
        int idInvestigadorSesion; // Variable para almacenar el ID del investigador que inició sesión

        public Boolean IniciarSesionComoAdmin(String correo_admin, string contrasenia_admin)
        {
            SqlCommand Consulta; // Variable de tipo SqlCommand para ejecutar la consulta SQL
            Consulta = new SqlCommand("select  correo_admin, contrasenia_admin from ADMINISTRADOR where correo_admin = @correo_admin and contrasenia_admin = @contrasenia_admin", cn.Conectar()); // Consulta SQL para verificar el inicio de sesión
            Consulta.CommandType = CommandType.Text; // Tipo de comando SQL
            Consulta.Parameters.AddWithValue("@correo_admin", correo_admin); // Agregar el parámetro correo admin a la consulta SQL
            Consulta.Parameters.AddWithValue("@contrasenia_admin", contrasenia_admin); // Agregar el parámetro PasswordUsuario a la consulta SQL

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Consulta); // Variable de tipo SqlDataAdapter para llenar el DataSet con los datos obtenidos de la consulta SQL
                da.Fill(ds, "ADMINISTRADOR"); // Llenar el DataSet con los datos obtenidos de la consulta SQL
                DataRow dr; // Variable de tipo DataRow para almacenar una fila del DataSet
                dr = ds.Tables["ADMINISTRADOR"].Rows[0]; // Almacenar la primera fila del DataSet en la variable dr
                if (Convert.ToString(correo_admin) == dr["correo_admin"].ToString() && contrasenia_admin == dr["contrasenia_admin"].ToString()) // Verificar si el correo y contrasenia coinciden con los datos obtenidos de la consulta SQL
                {
                    MessageBox.Show("Bienvenido Administrador"); // Si coinciden, mostrar un mensaje de bienvenida para el administrador
                    MenuInicialAdministrador formAdministrador = new MenuInicialAdministrador(); // Crear una instancia del formulario de administrador
                    formAdministrador.Show(); // Mostrar el formulario de administrador
                    estado_coneccion = true; // Si coinciden, establecer el estado de la conexión como verdadero
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario o Contraseña Incorrecta", "FORMULARIO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cn.Cerrar();
            }
            return estado_coneccion; 
        }

        public Boolean IniciarSesionComoInvestigador(String correo_inv, string contrasenia_inv)
        {
            SqlCommand Consulta = new SqlCommand("SELECT idInv, correo_inv, contrasenia_inv, tipo_inv FROM INVESTIGADOR WHERE correo_inv = @correo_inv AND contrasenia_inv = @contrasenia_inv", cn.Conectar());
            Consulta.Parameters.AddWithValue("@correo_inv", correo_inv);
            Consulta.Parameters.AddWithValue("@contrasenia_inv", contrasenia_inv);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Consulta);
                ds.Clear(); // Limpiar el DataSet antes de llenar
                da.Fill(ds, "INVESTIGADOR");

                if (ds.Tables["INVESTIGADOR"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["INVESTIGADOR"].Rows[0];

                    if (correo_inv == dr["correo_inv"].ToString() && contrasenia_inv == dr["contrasenia_inv"].ToString())
                    {
                        if ("LIDER" == dr["tipo_inv"].ToString())
                        {
                            idLiderSesion = Convert.ToInt32(dr["idInv"]);

                            MessageBox.Show("Bienvenido LÍDER");
                            cn.Cerrar();
                            MenuPrincipalLider formLider = new MenuPrincipalLider(idLiderSesion);
                            formLider.Show();

                            estado_coneccion = true;
                        }
                        else if ("INVESTIGADOR" == dr["tipo_inv"].ToString())
                        {
                            MessageBox.Show("Bienvenido INVESTIGADOR");
                            cn.Cerrar();
                            idInvestigadorSesion = Convert.ToInt32(dr["idInv"]);
                            menuinicialinvestigador MenuInvestigador = new menuinicialinvestigador(idInvestigadorSesion);
                            MenuInvestigador.Show();
                            estado_coneccion = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña Incorrecta");
                    cn.Cerrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                cn.Cerrar();
            }
            return estado_coneccion;
        }
    }
}