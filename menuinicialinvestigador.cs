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
    public partial class menuinicialinvestigador : Form
    {
        public menuinicialinvestigador()
        {
            InitializeComponent();
        }

        private void menuinicialinvestigador_Load(object sender, EventArgs e)
        {

        }

        private void btnMiSemilleroInvestigador_Click(object sender, EventArgs e)
        {
            MenuSemilleroInvestigador frmsemillero = new MenuSemilleroInvestigador();
            frmsemillero.Show();
            this.Hide();
        }

        private void btnInicioInvestigador_Click(object sender, EventArgs e)
        {

        }

        private void btnProyectosInvestigador_Click(object sender, EventArgs e)
        {
            MenuProyectosInvestigador frmProyectosinvestigador = new MenuProyectosInvestigador();
            frmProyectosinvestigador.Show();
            this.Hide();
        }

        private void btnPerfilInvestigador_Click(object sender, EventArgs e)
        {
            PerfilInvestigador frmPerfilInvestigador = new PerfilInvestigador();
            frmPerfilInvestigador.Show();
            this.Hide();
        }
    }
}
