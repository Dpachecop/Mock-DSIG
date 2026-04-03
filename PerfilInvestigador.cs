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
    public partial class PerfilInvestigador : Form
    {
        public PerfilInvestigador()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void btnInicioInvestigador_Click(object sender, EventArgs e)
        {
            menuinicialinvestigador frminicialinvestigador = new menuinicialinvestigador();
            frminicialinvestigador.Show();
            this.Hide();
        }

        private void btnMiSemilleroInvestigador_Click(object sender, EventArgs e)
        {
            MenuSemilleroInvestigador frmsemillero = new MenuSemilleroInvestigador();
            frmsemillero.Show();
            this.Hide();
        }

        private void btnProyectosInvestigador_Click(object sender, EventArgs e)
        {
            MenuProyectosInvestigador frmProyectosinvestigador = new MenuProyectosInvestigador();
            frmProyectosinvestigador.Show();
            this.Hide();
        }
    }
}
