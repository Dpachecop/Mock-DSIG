using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mock_DSIG
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnInicioAdmin_Click(object sender, EventArgs e)
        {
            MenuInicialAdministrador menuInicialAdministrador = new MenuInicialAdministrador();
            menuInicialAdministrador.Show();
            this.Hide();
        }

        private void btnSemillerosAdmin2_Click(object sender, EventArgs e)
        {

        }

        private void btnProyectosAdmin_Click(object sender, EventArgs e)
        {
            Form3 frmProyectos = new Form3();
            frmProyectos.Show();
            this.Hide();
        }

        private void btnUsuariosAdmin_Click(object sender, EventArgs e)
        {
            Form4 frmUsuariosAdmin = new Form4(); 
            frmUsuariosAdmin.Show();
            this.Hide();
        }

        private void btnReportesAdmin_Click(object sender, EventArgs e)
        {
            Form5 frmReportes = new Form5();
            frmReportes.Show();
            this.Hide();
        }
    }
}
