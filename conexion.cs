using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mock_DSIG
{
    internal class conexion
    {
        SqlConnection con;

        public SqlConnection Conectar()
        {
                try
                {
                    String datasource = "DESKTOP-9S95AUB\\SQLEXPRESS";
                    String initialCatalog = "DSIG";

                    // Solo creamos la conexión si es nula
                    if (con == null)
                    {
                        con = new SqlConnection($"Data Source={datasource};Initial Catalog={initialCatalog};Integrated Security=True");
                    }

                    // Solo abrimos si está cerrada
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error de conexión: " + e.Message);
                }
                return con;
        }
        public void Cerrar() //Cerrar la conexión
        {
            con.Close();
        }

    }
}
