using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mock_DSIG
{
    public partial class MenuPrincipalLider : Form
    {
        conexion cn = new conexion();
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
        public void CargarDatosdelSemillero()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // consulta para contar proyectos especificamente del lider por eso el inner join con semillero y el filtro por idInv
                string queryProyectos = @"SELECT COUNT(*) 
                                  FROM PROYECTO P
                                  INNER JOIN SEMILLERO S ON P.SEMILLERO_idSEMILLERO = S.idSEMILLERO
                                  WHERE S.idInv = @idLider AND P.estado_proyecto = 'Habilitado'";

                SqlCommand cmd1 = new SqlCommand(queryProyectos, conexionAbierta);
                cmd1.Parameters.AddWithValue("@idLider", idLiderSesion);
                int totalProyectos = (int)cmd1.ExecuteScalar();
                lblProyectosActivos.Text = totalProyectos.ToString();

                // consulta para contar miembros del semillero del lider
                string queryMiembros = "SELECT COUNT(*) FROM INVESTIGADOR WHERE SEMILLERO_idSEMILLERO = (SELECT idSEMILLERO FROM SEMILLERO  WHERE idInv = @idLider)";
                SqlCommand cmd2 = new SqlCommand(queryMiembros, conexionAbierta);
                cmd2.Parameters.AddWithValue("@idLider", idLiderSesion);
                int totalMiembros = (int)cmd2.ExecuteScalar();
                lblmiembros.Text = totalMiembros.ToString();
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estadísticas: " + ex.Message);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }
        public void CargarTablaReportes()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // Consulta para traer los reportes del semillero del líder logueado
                string query = "SELECT idREPORTE AS [ID], nombre_reporte AS [Nombre], tipo_reporte AS [Tipo], fecha_reporte AS [Fecha], descripcion_reporte AS [Descripción] FROM REPORTE  WHERE SEMILLERO_idSEMILLERO = (SELECT idSEMILLERO FROM SEMILLERO WHERE idInv = @idLider)";
                SqlDataAdapter da = new SqlDataAdapter(query, conexionAbierta);
                da.SelectCommand.Parameters.AddWithValue("@idLider", idLiderSesion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridReportes.DataSource = dt;

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reportes: " + ex.Message);
            }
        }

        private void MenuPrincipalLider_Load(object sender, EventArgs e)
        {
            CargarDatosdelSemillero();
            CargarTablaReportes();
        }

        private void btnGenerarReportes_Click(object sender, EventArgs e)
        {
            PantallaReporteLider frmReporte = new PantallaReporteLider(this.idLiderSesion);
            frmReporte.ShowDialog(); // Usamos ShowDialog para esperar a que termine
            CargarTablaReportes();
        }
    }
}