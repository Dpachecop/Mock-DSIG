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
    public partial class MenuProyectosInvestigador : Form
    {
        public MenuProyectosInvestigador()
        {
            InitializeComponent();
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

        private void btnPerfilInvestigador_Click(object sender, EventArgs e)
        {
            PerfilInvestigador frmPerfilInvestigador = new PerfilInvestigador();
            frmPerfilInvestigador.Show();
            this.Hide();
        }
    }
}
