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
    public partial class Form1 : Form
    {


        consultas_login cn = new consultas_login();
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Debe Ingresar Datos Validos para Poder Iniciar Sesión", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                cn.IniciarSesionComoAdmin(textBox1.Text, textBox2.Text);
                this.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
