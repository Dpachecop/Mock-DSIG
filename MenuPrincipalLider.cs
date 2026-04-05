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
    public partial class MenuPrincipalLider : Form
    {
        int idLiderSesion; // variable para almacenar el ID del líder que inició sesión

        // ADICIÓN: Constructor por defecto para evitar errores del Diseñador
        public MenuPrincipalLider()
        {
            InitializeComponent();
        }

        public MenuPrincipalLider(int idRecibido)
        {
            InitializeComponent();
            this.idLiderSesion = idRecibido; // asignamos el ID recibido a la variable local
        }

        private void btnMiSemilleroLider_Click(object sender, EventArgs e)
        {
            // Pasamos el ID a la pantalla de Semillero
            PantallaSemilleroLider pantallaSemilleroLider = new PantallaSemilleroLider(this.idLiderSesion);
            pantallaSemilleroLider.Show();
            this.Hide();
        }

        private void btnProyectosLider_Click(object sender, EventArgs e)
        {
            // Pasamos el ID a la pantalla de Proyectos
            ProyectosLider proyectosLider = new ProyectosLider(this.idLiderSesion);
            proyectosLider.Show();
            this.Hide();
        }

        private void btnMiembrosLider_Click(object sender, EventArgs e)
        {
            // Pasamos el ID a la pantalla de Miembros
            PantallaMiembrosLIder pantallaMiembrosLIder = new PantallaMiembrosLIder(this.idLiderSesion);
            pantallaMiembrosLIder.Show();
            this.Hide();
        }

        private void btnInicioLider_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSalirLider_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Cerrar Sesión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnReportesLider_Click(object sender, EventArgs e)
        {
          
        }
    }
}