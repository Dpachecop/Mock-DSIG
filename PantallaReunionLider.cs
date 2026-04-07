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
    public partial class PantallaReunionLider : Form
    {
        int idLiderSesion;
        conexion cn = new conexion();
        public PantallaReunionLider(int idRecibido)
        {
            InitializeComponent();
            this.idLiderSesion = idRecibido;
        }

        private void PantallaReunionLider_Load(object sender, EventArgs e)
        {
            CargarReuniones();
        }
        public void CargarReuniones()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();

                // Consultamos las reuniones del semillero al que pertenece el líder
                string query = "SELECT idREUNION AS [ID], motivo_reunion AS [Motivo], fecha_reunion AS [Fecha] FROM REUNION WHERE SEMILLERO_idSEMILLERO = (SELECT SEMILLERO_idSEMILLERO FROM INVESTIGADOR WHERE idInv = @idLider)";

                SqlDataAdapter da = new SqlDataAdapter(query, conexionAbierta);
                da.SelectCommand.Parameters.AddWithValue("@idLider", idLiderSesion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridReu.DataSource = dt;
                dataGridReu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reuniones: " + ex.Message, "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }

        private void btnInicioLider_Click(object sender, EventArgs e)
        {
            MenuPrincipalLider menuPrincipalLider = new MenuPrincipalLider(this.idLiderSesion);
            menuPrincipalLider.Show();
            this.Hide();
        }

        private void btnMiSemilleroLider_Click(object sender, EventArgs e)
        {
            PantallaSemilleroLider pantallaSemilleroLider = new PantallaSemilleroLider(this.idLiderSesion);
            pantallaSemilleroLider.Show();
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
        }

        private void btnSalirLider_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Cerrar Sesión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnAggReu_Click(object sender, EventArgs e)
        {
            NuevaReunionLider frmAgregar = new NuevaReunionLider(idLiderSesion);
            frmAgregar.ShowDialog();
            CargarReuniones();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarReu_Click(object sender, EventArgs e)
        {
            // 1. Verificar que haya una fila seleccionada
            if (dataGridReu.CurrentRow != null)
            {
                // Extraemos el ID de la columna "ID" (el alias que pusimos en CargarReuniones)
                int idEliminar = Convert.ToInt32(dataGridReu.CurrentRow.Cells["ID"].Value);
                string motivo = dataGridReu.CurrentRow.Cells["Motivo"].Value.ToString();

                // 2. Pedir confirmación al usuario (Seguridad)
                DialogResult respuesta = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar la reunión: '{motivo}'?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        SqlConnection conexionAbierta = cn.Conectar();
                        string query = "DELETE FROM REUNION WHERE idREUNION = @id";

                        SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                        cmd.Parameters.AddWithValue("@id", idEliminar);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cn.Cerrar();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Reunión eliminada correctamente.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarReuniones(); // Refrescamos el DataGrid para que desaparezca
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message, "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila en la tabla para eliminar.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEditReu_Click(object sender, EventArgs e)
        {
            if (dataGridReu.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridReu.CurrentRow.Cells["ID"].Value);
                string motivo = dataGridReu.CurrentRow.Cells["Motivo"].Value.ToString();
                DateTime fecha = Convert.ToDateTime(dataGridReu.CurrentRow.Cells["Fecha"].Value);
                EditarReuLider frmEditar = new EditarReuLider(id, motivo, fecha);
                frmEditar.ShowDialog();
                CargarReuniones();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una reunión de la tabla para editar.");
            }
        }
    }
}
