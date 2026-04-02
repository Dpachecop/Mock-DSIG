using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mock_DSIG
{
    public partial class Form3 : Form
    {
        conexion cn = new conexion();
        DataSet ds = new DataSet();
        public Form3()
        {
            InitializeComponent();
        }
        public void ConsultarDatosProyectosAdmin()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT  P.idPROYECTO AS [ID], P.nombre_proyecto AS [Nombre Proyecto], I.nombre_inv + ' ' + I.apellido_inv AS [Investigador Líder], P.FECHA_INICIO AS [Fecha Inicio], P.fecha_final_proyecto AS [Fecha Final], P.objetivo_proyecto AS [Descripción] FROM PROYECTO P LEFT JOIN PROYECTO_INVESTIGADOR PI ON P.idPROYECTO = PI.idPROYECTO LEFT JOIN INVESTIGADOR I ON PI.idINVESTIGADOR = I.idInv"; 
                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                da.Fill(dt);

                dataGridProyectos.DataSource = null;
                dataGridProyectos.Columns.Clear();
                dataGridProyectos.DataSource = dt;
                dataGridProyectos.Refresh(); // Refresca y actualiza el datagrid por si se crean celdas vacias o si se agregan nuevos proyectos 

                cn.Cerrar();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("La consulta no devolvió filas. Verifica que existan datos en la tabla PROYECTO.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void btnSemillerosAdmin_Click(object sender, EventArgs e)
        {
            Form2 frmSemillerosAdmin = new Form2();
            frmSemillerosAdmin.Show();
            this.Hide();
        }

        private void btnInicioAdmin_Click(object sender, EventArgs e)
        {
            MenuInicialAdministrador menuInicialAdministrador = new MenuInicialAdministrador();
            menuInicialAdministrador.Show();
            this.Hide();
        }

        private void btnUsuariosAdmin_Click(object sender, EventArgs e)
        {
            Form4 frmUsuarios = new Form4();
            frmUsuarios.Show();
            this.Hide();
        }

        private void btnReportesAdmin_Click(object sender, EventArgs e)
        {
            Form5 frmReportes = new Form5();
            frmReportes.Show();
            this.Hide();
        }

        private void btnAggProyectos_Click(object sender, EventArgs e)
        {
            Form6 frmRegistroProyecto = new Form6();
            frmRegistroProyecto.ShowDialog(); 
        }

        private void btnConsultarProyectosAdmin_Click(object sender, EventArgs e)
        {
            ConsultarDatosProyectosAdmin();
        }

        private void btnSalirAdmin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que deseas salir?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
