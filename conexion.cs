using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Drawing;
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
                String datasource = "localhost";
                String initialCatalog = "DSIG";
                con = new SqlConnection($"Data Source={datasource};Initial Catalog={initialCatalog};Integrated Security=True"); // Cadena de conexión = Nombre Servidor - Nombre BD - Tipo de Seguridad
                con.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return con;


        }
        public void Cerrar() //Cerrar la conexión
        {
            con.Close();
        }

    }
}
