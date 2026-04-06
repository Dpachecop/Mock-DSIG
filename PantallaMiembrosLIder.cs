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
    public partial class PantallaMiembrosLIder : Form
    {
        int idLiderSesion;
        conexion cn = new conexion();
        public PantallaMiembrosLIder()
        {
            InitializeComponent();
        }

        public PantallaMiembrosLIder(int idRecibido)
        {
            InitializeComponent();
            this.idLiderSesion = idRecibido; // asignamos el ID recibido a la variable local
        }

        private void btnInicioLider_Click(object sender, EventArgs e)
        {
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
        public void CargarTablaMiembros()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();

                // Consulta que trae al Líder y a sus Investigadores vinculados
                string query = @"SELECT idInv AS [ID], 
                                        nombre_inv AS [Nombre], 
                                        apellido_inv AS [Apellido], 
                                        correo_inv AS [Correo], 
                                        numero_tel_inv AS [Teléfono],
                                        tipo_inv AS [Rol]
                                 FROM INVESTIGADOR 
                                 WHERE (SEMILLERO_idSEMILLERO = (SELECT idSEMILLERO FROM SEMILLERO WHERE idInv = @idLider) 
                                        OR idInv = @idLider)";

                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@idLider", idLiderSesion);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Asignamos los datos a tu DataGridView
                dataGridUsuariosLider.DataSource = dt;

                // Formato visual para resaltar al líder
                foreach (DataGridViewRow row in dataGridUsuariosLider.Rows)
                {
                    if (row.Cells["Rol"].Value != null && row.Cells["Rol"].Value.ToString() == "LIDER")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 255);
                        row.DefaultCellStyle.Font = new Font(dataGridUsuariosLider.Font, FontStyle.Bold);
                    }
                }

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar: " + ex.Message);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }
        public void DesvincularMIembro(string id)
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                string query = "UPDATE INVESTIGADOR SET SEMILLERO_idSEMILLERO = NULL WHERE idInv = @id";
                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@id", id);

                int filas = cmd.ExecuteNonQuery();

                if (filas > 0)
                {
                    MessageBox.Show("Investigador desvinculado con éxito.", "Hecho",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refrescamos la tabla para que ya no aparezca en la lista
                    CargarTablaMiembros();
                }

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al desvincular: " + ex.Message);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }

        private void btnMiembrosLider_Click(object sender, EventArgs e)
        {
         
        }

        private void btnMiSemilleroLider_Click(object sender, EventArgs e)
        {
            PantallaSemilleroLider pantallaSemilleroLider = new PantallaSemilleroLider(this.idLiderSesion);
            pantallaSemilleroLider.Show();
            this.Hide();
        }

        private void btnReportesLider_Click(object sender, EventArgs e)
        {
          
        }

        private void btnConsultarAdmin_Click(object sender, EventArgs e)
        {
            CargarTablaMiembros();  
        }

        private void PantallaMiembrosLIder_Load(object sender, EventArgs e)
        {

        }

        private void btnEliminarUsuariosLider_Click(object sender, EventArgs e)
        {
            // Verificamos que el usuario haya seleccionado una fila
            if (dataGridUsuariosLider.SelectedRows.Count > 0)
            {
                // Extraemos los datos de la fila seleccionada
                string idSeleccionado = dataGridUsuariosLider.CurrentRow.Cells["ID"].Value.ToString();
                string nombre = dataGridUsuariosLider.CurrentRow.Cells["Nombre"].Value.ToString();
                string rol = dataGridUsuariosLider.CurrentRow.Cells["Rol"].Value.ToString();

                // el lider no se puede desvincular el mismo asi que se valida eso
                if (rol == "LIDER")
                {
                    MessageBox.Show("No puedes desvincular al líder (tú mismo) desde esta pantalla.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                DialogResult result = MessageBox.Show($"¿Estás seguro de desvincular a {nombre} del semillero?\r\n" +
                    "(El investigador no se borrará del sistema, solo dejará de pertenecer a este grupo).",
                    "Confirmar Desvinculación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DesvincularMIembro(idSeleccionado);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila completa en la tabla primero.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAggUsuariosLider_Click(object sender, EventArgs e)
        {
            VincularMiembroLider vincularMiembroLider = new VincularMiembroLider(this.idLiderSesion);
            vincularMiembroLider.ShowDialog();
        }
    }
}