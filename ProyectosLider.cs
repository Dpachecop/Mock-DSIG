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
    public partial class ProyectosLider : Form
    {
        // Instanciacion de Clases
        conexion cn = new conexion();
        DataSet ds = new DataSet();
        public ProyectosLider()
        {
            InitializeComponent();
        }

        private void btnInicioLider_Click(object sender, EventArgs e)
        {
            MenuPrincipalLider menuPrincipalLider = new MenuPrincipalLider();
            menuPrincipalLider.Show();
            this.Hide();
        }

        private void btnMiSemilleroLider_Click(object sender, EventArgs e)
        {
            PantallaSemilleroLider pantallaSemilleroLider = new PantallaSemilleroLider();
            pantallaSemilleroLider.Show();
            this.Hide();
        }

        private void btnMiembrosLider_Click(object sender, EventArgs e)
        {
            PantallaMiembrosLIder pantallaMiembrosLIder = new PantallaMiembrosLIder();
            pantallaMiembrosLIder.Show();
            this.Hide();
        }

        private void btnAggProyectosLider_Click(object sender, EventArgs e)
        {
            Form6 AgregarProyectos = new Form6();
            AgregarProyectos.Show();
        }

        public void ConsultarDatosProyectosLider()
        {
            SqlCommand Consulta2; // Tipo de Comando para ejecutar la consulta SQL (Procedimiento 
            Consulta2 = new SqlCommand("select * from PROYECTO", cn.Conectar());
            Consulta2.CommandType = CommandType.Text;
            Consulta2.ExecuteNonQuery();
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(Consulta2);
            da.Fill(ds, "PROYECTO");
            try
            {
                dataGridProyectosLider.DataMember = ("PROYECTO");
                dataGridProyectosLider.DataSource = ds;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnConsultarAdmin_Click(object sender, EventArgs e)
        {
           
        }
    }
}
