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
    public partial class DetallesProyectosLider : Form
    {
        int idProyectoSeleccionado; // variable para almacenar el ID del proyecto seleccionado
        conexion cn = new conexion();
        public DetallesProyectosLider(int idProyecto, string nombreProyecto)
        {
            InitializeComponent();
            this.idProyectoSeleccionado = idProyecto;
            lblNombreProyecto.Text = nombreProyecto;
            CargarDataGrids();
        }

        private void CargarDataGrids()
        {
            try
            {
                // CONSULTA PARA CARGAR FASES
                SqlConnection conexionAbierta = cn.Conectar();
                string queryFases = @"SELECT idFASES AS [ID], nombre_fase AS [NOMBRE], fecha_inicio_fase AS [FECHA INICIO], descripcion_fase AS [DESCRIPCIÓN] FROM FASES  WHERE PROYECTO_idPROYECTO = @idProyecto";
                SqlCommand cmdFases = new SqlCommand(queryFases, conexionAbierta);
                cmdFases.Parameters.AddWithValue("@idProyecto", idProyectoSeleccionado);
                SqlDataAdapter daFases = new SqlDataAdapter(cmdFases);
                DataTable dtFases = new DataTable();
                daFases.Fill(dtFases);
                dataGridFases.DataSource = dtFases;
                // unimos actividades con fases para poder saber a que proyecto pertenecen, por eso el inner join
                string queryActividades = @"SELECT 
                                                A.idACTIVIDADES AS [ID Actividad], 
                                                F.nombre_fase AS [Fase Perteneciente], 
                                                A.nombre_actividad AS [Actividad], 
                                                A.descripcion_actividad AS [Descripción], 
                                                A.tipo_actividad AS [Tipo],
                                                A.estado_actividad AS [Estado]
                                            FROM ACTIVIDADES A
                                            INNER JOIN FASES F ON A.FASES_idFASES = F.idFASES
                                            WHERE F.PROYECTO_idPROYECTO = @idProyecto";
                SqlCommand cmdActividades = new SqlCommand(queryActividades, conexionAbierta);
                cmdActividades.Parameters.AddWithValue("@idProyecto", idProyectoSeleccionado);
                SqlDataAdapter daActividades = new SqlDataAdapter(cmdActividades);
                DataTable dtActividades = new DataTable();
                daActividades.Fill(dtActividades);
                dataGridActividades.DataSource = dtActividades;

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cn.Cerrar();
            }
        }

        private void btnCERRAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}