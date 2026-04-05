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
    public partial class Form4 : Form
    {
        conexion cn = new conexion();

        public Form4()
        {
            InitializeComponent();   
        }
        public void CargarUsuarios()
        {
            try
            {
                // esta consulta trae todos los investigadores y su semillero asociado, si no tienen semillero muestra Sin Asignar(cambiaria si despues se le asigna uno)
                string query = @"SELECT 
                    I.idInv AS [ID], 
                    I.nombre_inv AS [Nombre], 
                    I.apellido_inv AS [Apellido], 
                    I.correo_inv AS [Correo], 
                    I.numero_tel_inv AS [Teléfono], 
                    I.tipo_inv AS [Rol],
                    I.contrasenia_inv,
                    ISNULL(S.nombre_semillero, 'Sin Asignar') AS [Semillero]
                 FROM INVESTIGADOR I
                 LEFT JOIN SEMILLERO S ON I.idInv = S.idInv"; // el left join sirve para traer todos los investigadores aunque no tengan semillero asignado si no solo se trae los usuarios que ya tengan semillero
                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridUsuarios.DataSource = dt;
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los investigadores: " + ex.Message);
                cn.Cerrar();
            }
        }
        public void EliminarUSuarios()
        {
            if (dataGridUsuarios.SelectedRows.Count > 0)
            {
                try
                {
                    // capturamos los datos usando el nombre de la columna definido en el SELECT
                    int idInv = Convert.ToInt32(dataGridUsuarios.CurrentRow.Cells["ID"].Value);
                    string nombre = dataGridUsuarios.CurrentRow.Cells["Nombre"].Value.ToString();
                    string apellido = dataGridUsuarios.CurrentRow.Cells["Apellido"].Value.ToString();

                    DialogResult confirmacion = MessageBox.Show(
                        $"¿Está seguro de eliminar al investigador {nombre} {apellido}?\n\nID: {idInv}",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirmacion == DialogResult.Yes)
                    {
                        SqlConnection conexionAbierta = cn.Conectar();
                        // sql bloqueara esto si el usuario es LÍDER en la tabla SEMILLERO por un error de integridad referencial (busquenlo depues)
                        // tambien por un tema de jerarquia, primero se debe borrar el semillero para que el lider quede libre y se convierta en investigador, ahi si se podrá borrar
                        string borrarUsuario = "DELETE FROM INVESTIGADOR WHERE idInv = @id";
                        SqlCommand cmd = new SqlCommand(borrarUsuario, conexionAbierta);
                        cmd.Parameters.AddWithValue("@id", idInv);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Investigador eliminado con éxito.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        cn.Cerrar();
                        CargarUsuarios(); 
                    }
                }
                catch (SqlException ex)
                {
                    // Error 547: Restricción de llave foránea (Integridad Referencial)
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("No se puede eliminar: Este investigador está asignado como Líder de un semillero.\n\nDebe eliminar o reasignar el semillero primero.",
                            "Error de Dependencia", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        MessageBox.Show("Error de SQL: " + ex.Message);
                    }
                    cn.Cerrar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa en la tabla.", "Selección Requerida");
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnInicioAdmin_Click(object sender, EventArgs e)
        {
            MenuInicialAdministrador menuInicialAdministrador = new MenuInicialAdministrador();
            menuInicialAdministrador.Show();
            this.Hide();
        }

        private void btnProyectosAdmin_Click(object sender, EventArgs e)
        {
            Form3 frmProyectos = new Form3();
            frmProyectos.Show();
            this.Hide();
        }

        private void btnUsuariosAdmin2_Click(object sender, EventArgs e)
        {

        }

        private void btnSemillerosAdmin_Click(object sender, EventArgs e)
        {
            Form2 frmSemillerosAdmin = new Form2();
            frmSemillerosAdmin.Show();
            this.Hide();
        }

        private void btnReportesAdmin_Click(object sender, EventArgs e)
        {
            Form5 frmReportes = new Form5();
            frmReportes.Show();
            this.Hide();
        }

        private void btnSalirAdmin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Cerrar Sesión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                principal_screen principal_Screen = new principal_screen();
                principal_Screen.Show();
                this.Hide();
            }
        }

        private void btnAgregarNuevoUsuario_Click(object sender, EventArgs e)
        {
            screen_agg_inv screen_Agg_Inv = new screen_agg_inv();
            screen_Agg_Inv.ShowDialog();
        }

        private void btnConsultarUsuariosAdmin_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            EliminarUSuarios();
        }

        private void btnEditUsuario_Click(object sender, EventArgs e)
        {
            if (dataGridUsuarios.SelectedRows.Count > 0)
            {
                // Extraemos los datos de las celdas
                int id = Convert.ToInt32(dataGridUsuarios.CurrentRow.Cells[0].Value);
                string nombre = dataGridUsuarios.CurrentRow.Cells[1].Value.ToString();
                string apellido = dataGridUsuarios.CurrentRow.Cells[2].Value.ToString();
                string correo = dataGridUsuarios.CurrentRow.Cells[3].Value.ToString();
                string telefono = dataGridUsuarios.CurrentRow.Cells[4].Value.ToString();
                string pass = dataGridUsuarios.CurrentRow.Cells[5].Value.ToString();

                // Llamamos al formulario enviando los 6 parámetros
                EditarUsuarioAdmin frmEdit = new EditarUsuarioAdmin(id, nombre, apellido, correo, telefono, pass);
                frmEdit.ShowDialog();

                // Refrescamos la tabla al volver
                CargarUsuarios();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.");
            }
        }
    }
}
