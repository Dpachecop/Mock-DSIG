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
    public partial class ProyectosLider : Form
    {
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
    }
}
