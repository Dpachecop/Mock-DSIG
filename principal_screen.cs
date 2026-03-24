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
    public partial class principal_screen : Form
    {


        consultas_login cn = new consultas_login();
        public principal_screen()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            FormAdmin frmIniciarSesion = new FormAdmin();
            frmIniciarSesion.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormUsuario frmIniciarSesion = new FormUsuario();
            frmIniciarSesion.Show();
            this.Hide();
        }
    }
}
