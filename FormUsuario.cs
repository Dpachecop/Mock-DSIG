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
    public partial class FormUsuario : Form
    {
        public FormUsuario()
        {
            InitializeComponent();
        }


        consultas_login cn = new consultas_login();

        private void button1_Click(object sender, EventArgs e)
        {
            if (form_correo.Text == "" || form_password.Text == "")
            {
                MessageBox.Show("Debe Ingresar Datos Validos para Poder Iniciar Sesión", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                cn.IniciarSesionComoInvestigador(form_correo.Text, form_password.Text);
                this.Hide();
            }
        }
    }
}
