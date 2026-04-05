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
    public partial class ProyectosLider : Form
    {
        // Instanciacion de Clases
        conexion cn = new conexion();
        int idLiderSesion;

        public ProyectosLider(int idRecibido)
        {
            InitializeComponent();
            this.idLiderSesion = idRecibido;
        }

        public void CargarProyectos()
        {
            try
            {
               
                Console.WriteLine("Cargando proyectos para el Lider ID: " + idLiderSesion);

                SqlConnection conexionAbierta = cn.Conectar();

                string query = @"SELECT P.idPROYECTO AS [ID], S.nombre_semillero AS [SEMILLERO], P.nombre_proyecto AS [NOMBRE PROYECTO], P.FECHA_INICIO AS [FECHA INICIO], P.estado_proyecto AS [ESTADO PROYECTO], P.objetivo_proyecto AS [OBJETIVO PROYECTO]
                 FROM PROYECTO P
                 INNER JOIN SEMILLERO S ON P.SEMILLERO_idSEMILLERO = S.idSEMILLERO
                 WHERE S.idInv = @id";

                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@id", idLiderSesion);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridProyectosLider.DataSource = dt;
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de SQL: " + ex.Message);
                cn.Cerrar();
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

        private void btnConsultarAdmin_Click(object sender, EventArgs e)
        {
            CargarProyectos();
        }

        private void ProyectosLider_Load(object sender, EventArgs e)
        {
        }

        private void btnAggProyectosLider_Click(object sender, EventArgs e)
        {
            Form6 AgregarProyectos = new Form6();
            AgregarProyectos.Show();
        }

        private void btnMiembrosLider_Click(object sender, EventArgs e)
        {

        }

        private void btnEditProyectosLider_Click(object sender, EventArgs e)
        {
            if (dataGridProyectosLider.SelectedRows.Count > 0)
            {
                int idSeleccionado = Convert.ToInt32(dataGridProyectosLider.CurrentRow.Cells["ID"].Value);
                EditarProyectosLider frmEditar = new EditarProyectosLider(idSeleccionado);
                frmEditar.ShowDialog();
                CargarProyectos();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila completa haciendo clic en el extremo izquierdo de la tabla.", "Selección requerida");
            }
        }

        private void btnEliminarProyectosLider_Click(object sender, EventArgs e)
        {
            // 1. Verificamos si hay al menos una fila seleccionada en el DataGridView
            if (dataGridProyectosLider.SelectedRows.Count > 0)
            {
                // 2. Extraemos el ID del proyecto de la fila seleccionada
                // Usamos "ID" porque ese es el alias que le pusiste en CargarProyectos()
                int idProyectoSeleccionado = Convert.ToInt32(dataGridProyectosLider.CurrentRow.Cells["ID"].Value);

                // 3. Pedimos confirmación al usuario para evitar borrados por accidente
                DialogResult dialogResult = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este proyecto? Esta acción no se puede deshacer.",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        SqlConnection conexionAbierta = cn.Conectar();

                        // 4. Consulta blindada: Solo elimina si el proyecto y el líder coinciden
                        string query = @"DELETE FROM PROYECTO 
                                 WHERE idPROYECTO = @idProyecto 
                                 AND SEMILLERO_idSEMILLERO = (SELECT idSEMILLERO FROM SEMILLERO WHERE idInv = @idLider)";

                        SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                        cmd.Parameters.AddWithValue("@idProyecto", idProyectoSeleccionado);
                        cmd.Parameters.AddWithValue("@idLider", idLiderSesion);

                        // Ejecutamos la consulta y guardamos cuántas filas se borraron
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        cn.Cerrar();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Proyecto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProyectos(); // Refrescamos la tabla al instante
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar. Es posible que el proyecto no te pertenezca o ya no exista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        // 5. Control de errores de Llaves Foráneas
                        cn.Cerrar();
                        if (sqlEx.Number == 547)
                        {
                            MessageBox.Show("No puedes eliminar este proyecto porque ya tiene Fases o Participaciones asociadas. Debes eliminar esos registros primero.", "Error de Dependencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Error en la base de datos: " + sqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cn.Cerrar();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona toda la fila del proyecto que deseas eliminar haciendo clic en el margen izquierdo de la tabla.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btndetalles_Click(object sender, EventArgs e)
        {
            if (dataGridProyectosLider.SelectedRows.Count > 0)
            {
                int idProy = Convert.ToInt32(dataGridProyectosLider.CurrentRow.Cells["ID"].Value);
                string nombreProy = dataGridProyectosLider.CurrentRow.Cells["NOMBRE PROYECTO"].Value.ToString();
                DetallesProyectosLider frmDetalles = new DetallesProyectosLider(idProy, nombreProy);
                frmDetalles.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecciona una fila para ver los detalles.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Verificamos que haya un proyecto seleccionado en la tabla
            if (dataGridProyectosLider.SelectedRows.Count > 0)
            {
                // Obtenemos el ID de la celda "ID" de la fila seleccionada
                int idProy = Convert.ToInt32(dataGridProyectosLider.CurrentRow.Cells["ID"].Value);

                // Abrimos el formulario enviándole el ID
                AgregarFaseLider frmFase = new AgregarFaseLider(idProy);
                frmFase.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un proyecto de la lista para añadirle una fase.", "Aviso");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridProyectosLider.SelectedRows.Count > 0)
            {
                int idProy = Convert.ToInt32(dataGridProyectosLider.CurrentRow.Cells["ID"].Value);
                AgregarActividadLider frmAct = new AgregarActividadLider(idProy);
                frmAct.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un proyecto para añadirle una actividad.");
            }
        }
    }
    
}