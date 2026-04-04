using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mock_DSIG
{
    public partial class MenuInicialAdministrador : Form
    {
        conexion cn = new conexion();
        public MenuInicialAdministrador()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MenuInicialAdministrador_Load(object sender, EventArgs e)
        {
            ActualizarCantidades();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnInicioAdmin_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void btnSemillerosAdmin_Click(object sender, EventArgs e)
        {
            Form2 frmSemillerosAdmin = new Form2();
            frmSemillerosAdmin.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void btnProyectosAdmin_Click(object sender, EventArgs e)
        {
            Form3 frmProyectosAdmin = new Form3();
            frmProyectosAdmin.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void btnUsuariosAdmin_Click(object sender, EventArgs e)
        {
            Form4 frmUsuariosAdmin = new Form4();
            frmUsuariosAdmin.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void btnReportesAdmin_Click(object sender, EventArgs e)
        {
            Form5 frmReportes = new Form5();
            frmReportes.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void btnSalirAdmin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Cerrar Sesión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                principal_screen principal_Screen = new principal_screen();
                principal_Screen.Show();
                this.Hide();
            }
        }
        public void ActualizarCantidades()
        {
            try
            {
                // Aca hacemos una consulta simple para contabilizar la cantidad de semilleros, proyectos e investigadores y mostrarlo en los labels correspondientes
                SqlConnection conexionAbierta = cn.Conectar();

                string qSemilleros = "SELECT COUNT(*) FROM SEMILLERO"; 
                SqlCommand cmd1 = new SqlCommand(qSemilleros, conexionAbierta);
                lblSemillerossActivos.Text = cmd1.ExecuteScalar().ToString();


                string qProyectos = "SELECT COUNT(*) FROM PROYECTO";
                SqlCommand cmd2 = new SqlCommand(qProyectos, conexionAbierta);
                lblProyectosAdmin.Text = cmd2.ExecuteScalar().ToString();

                string qInvestigadores = "SELECT COUNT(*) FROM INVESTIGADOR";
                SqlCommand cmd3 = new SqlCommand(qInvestigadores, conexionAbierta);
                lblInvAdmin.Text = cmd3.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estadísticas: " + ex.Message);
            }
        }

        private void MenuInicialAdministrador_Activated(object sender, EventArgs e)
        {
            ActualizarCantidades();
        }
    }
    
}
