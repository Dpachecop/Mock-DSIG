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
        // Usamos el mismo nombre de variable para mantener coherencia en todo el proyecto
        int idLiderSesion;
        public PantallaSemilleroLider()
        {
            InitializeComponent();
        }
        public PantallaSemilleroLider(int idRecibido)
        {
            InitializeComponent();
            this.idLiderSesion = idRecibido;
        }

        private void btnInicioLider_Click(object sender, EventArgs e)
        {
            // Devolvemos el ID al menú al regresar
            MenuPrincipalLider menuPrincipalLider = new MenuPrincipalLider(this.idLiderSesion);
            menuPrincipalLider.Show();
            this.Hide();
        }

        private void btnProyectosLider_Click(object sender, EventArgs e)
        {
            ProyectosLider proyectosLider = new ProyectosLider(this.idLiderSesion);
            proyectosLider.Show();
            this.Hide();
        }

        private void btnMiembrosLider_Click(object sender, EventArgs e)
        {
            PantallaMiembrosLIder pantallaMiembrosLIder = new PantallaMiembrosLIder(this.idLiderSesion);
            pantallaMiembrosLIder.Show();
            this.Hide();
        }

        private void btnSalirLider_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Cerrar Sesión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void dTPFechar_ValueChanged(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void btnInicio_Click(object sender, EventArgs e) { btnInicioLider_Click(sender, e); }
        private void btnProyectos_Click(object sender, EventArgs e) { btnProyectosLider_Click(sender, e); }
    }
}