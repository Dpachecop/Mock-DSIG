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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnInicioAdmin_Click(object sender, EventArgs e)
        {
            MenuInicialAdministrador menuInicialAdministrador = new MenuInicialAdministrador();
            menuInicialAdministrador.Show();
            this.Hide();
        }

        private void btnProyectosAdmin_Click(object sender, EventArgs e)
        {

        }
    }
}
