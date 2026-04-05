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
    public partial class PantallaMiembrosLIder : Form
    {
        public PantallaMiembrosLIder()
        {
            InitializeComponent();
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

        private void btnMiSemilleroLider_Click(object sender, EventArgs e)
        {
            PantallaSemilleroLider pantallaSemilleroLider = new PantallaSemilleroLider();
            pantallaSemilleroLider.Show();
            this.Hide();
        }

        private void btnReportesLider_Click(object sender, EventArgs e)
        {

        }
    }
}
