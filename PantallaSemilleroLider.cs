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
    public partial class PantallaSemilleroLider : Form
    {
        public PantallaSemilleroLider()
        {
            InitializeComponent();
        }

        private void dTPFechar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            MenuPrincipalLider menuPrincipalLider = new MenuPrincipalLider();
            menuPrincipalLider.Show();
            this.Hide();
        }

        private void btnProyectos_Click(object sender, EventArgs e)
        {
            ProyectosLider proyectosLider = new ProyectosLider();
            proyectosLider.Show();
            this.Hide();
        }

        private void btnMiSemilleroInvestigador_Click(object sender, EventArgs e)
        {

        }

        private void btnInicioInvestigador_Click(object sender, EventArgs e)
        {

        }

        private void btnProyectosInvestigador_Click(object sender, EventArgs e)
        {

        }

        private void btnSalirLider_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Cerrar Sesión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnInicioLider_Click(object sender, EventArgs e)
        {
            MenuPrincipalLider menuPrincipalLider = new MenuPrincipalLider();
            menuPrincipalLider.Show();
            this.Hide();
        }

        private void btnProyectosLider_Click(object sender, EventArgs e)
        {
            ProyectosLider proyectosLider = new ProyectosLider();
            proyectosLider.Show();
            this.Hide();
        }

        private void btnMiembrosLider_Click(object sender, EventArgs e)
        {
            PantallaMiembrosLIder pantallaMiembrosLIder = new PantallaMiembrosLIder();
            pantallaMiembrosLIder.Show();
            this.Hide();
        }
    }
}
