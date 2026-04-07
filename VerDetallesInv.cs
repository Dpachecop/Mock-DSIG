using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Mock_DSIG
{
    public partial class VerDetallesInv : Form
    {
        conexion cn = new conexion();
        int idProyectoRecibido;

        public VerDetallesInv()
        {
            InitializeComponent();
        }

        public VerDetallesInv(int idProyecto)
        {
            InitializeComponent();
            this.idProyectoRecibido = idProyecto;
        }

        public void VerFasesyActividades()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                // consulta para traer el nombre del proyecto
                string queryNombre = "SELECT nombre_proyecto FROM PROYECTO WHERE idPROYECTO = @id";
                SqlCommand cmdNombre = new SqlCommand(queryNombre, conexionAbierta);
                cmdNombre.Parameters.AddWithValue("@id", idProyectoRecibido);
                var resultado = cmdNombre.ExecuteScalar();
                lblNombreProyecto.Text = resultado != null ? resultado.ToString() : "Proyecto no encontrado";
                // consulta para traer las fases relacionadas al proyecto
                string queryFases = @"SELECT idFASES AS [ID], nombre_fase AS [Fase], fecha_inicio_fase AS [Inicio], descripcion_fase AS [Descripción] FROM FASES WHERE PROYECTO_idPROYECTO = @id";
                SqlDataAdapter daFases = new SqlDataAdapter(queryFases, conexionAbierta);
                daFases.SelectCommand.Parameters.AddWithValue("@id", idProyectoRecibido);
                DataTable dtFases = new DataTable();
                daFases.Fill(dtFases);
                dataGridFases.DataSource = dtFases;
                // consulta para traer las actividades relacionadas a las fases del proyecto
                string queryAct = @"SELECT A.idACTIVIDADES AS [ID], A.nombre_actividad AS [Actividad], 
                                    F.nombre_fase AS [Fase Relacionada], A.tipo_actividad AS [Tipo], A.descripcion_actividad AS [Descripción]
                                    FROM ACTIVIDADES A 
                                    INNER JOIN FASES F ON A.FASES_idFASES = F.idFASES 
                                    WHERE F.PROYECTO_idPROYECTO = @id";
                // por eso el inner join 
                SqlDataAdapter daAct = new SqlDataAdapter(queryAct, conexionAbierta);
                daAct.SelectCommand.Parameters.AddWithValue("@id", idProyectoRecibido);
                DataTable dtAct = new DataTable();
                daAct.Fill(dtAct);
                dataGridActividades.DataSource = dtAct;

                cn.Cerrar();

                dataGridFases.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridActividades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en SQL: " + ex.Message, "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }

        private void btnCERRAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VerDetallesInv_Load(object sender, EventArgs e)
        {
            VerFasesyActividades();
        }
    }
}