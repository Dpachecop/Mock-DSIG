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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnInicioAdmin_Click(object sender, EventArgs e)
        {
            MenuInicialAdministrador menuInicialAdministrador = new MenuInicialAdministrador();
            menuInicialAdministrador.Show();
            this.Hide();
        }

        private void btnSemillerosAdmin_Click(object sender, EventArgs e)
        {
            Form2 frmSemilleros = new Form2();
            frmSemilleros.Show();
            this.Hide();
        }

        private void btnProyectosAdmin_Click(object sender, EventArgs e)
        {
            Form3 frmProyectos = new Form3();
            frmProyectos.Show();
            this.Hide();
        }

        private void btnUsuariosAdmin_Click(object sender, EventArgs e)
        {
            Form4 frmUsuarios = new Form4();
            frmUsuarios.Show();
            this.Hide();
        }

        private void btnReportesAdmin_Click(object sender, EventArgs e)
        {

        }
    }
}
