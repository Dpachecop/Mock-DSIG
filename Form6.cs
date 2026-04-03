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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Mock_DSIG
{
    public partial class Form6 : Form
    {
        // Instanciacion de Clases
        conexion cn = new conexion();
        DataSet ds = new DataSet();
        public Form6()
        {
            InitializeComponent();
            LlenarComboSemilleros(); // Llenar el ComboBox con los semilleros disponibles al cargar el formulario
            LlenarComboLideres(); // Llenar el ComboBox con los líderes registrados al cargar el formulario
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtidPROYECTO.Text) || comboBoxLider.SelectedValue == null)
            {
                MessageBox.Show("Faltan datos obligatorios.");
                return;
            }

            try
            {
                // A. Captura de datos
                int idProy = int.Parse(txtidPROYECTO.Text);
                int idSemi = Convert.ToInt32(comboBoxSemilleros.SelectedValue);
                int idLider = Convert.ToInt32(comboBoxLider.SelectedValue); 
                string nombre = txtNombreProyecto.Text;
                DateTime inicio = dateTimePickerInicio.Value;
                DateTime final = dateTimePickerFinal.Value;
                string objetivo = txtDescripcionProyecto.Text;
                string estadoSeleccionado = comboBoxEstadoProyecto.SelectedItem.ToString();

                InsertarProyectos(idProy, idSemi, nombre, inicio, final, objetivo, estadoSeleccionado);
                VincularProyectoConInvestigador(idProy, idLider);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general en el proceso: " + ex.Message);
            }
        }
        
        


        public void InsertarProyectos(int idPROYECTO, int SEMILLERO_idSEMILLERO, string nombre_proyecto, DateTime FECHA_INICIO, DateTime fecha_final_proyecto, string objetivo_proyecto, string estado)
        {
            try
            {
              
                SqlCommand Insert = new SqlCommand("INSERT INTO PROYECTO (idPROYECTO, SEMILLERO_idSEMILLERO, nombre_proyecto, FECHA_INICIO, fecha_final_proyecto, objetivo_proyecto, estado_proyecto) " + "VALUES (@idPROYECTO, @SEMILLERO_idSEMILLERO, @nombre_proyecto, @FECHA_INICIO, @fecha_final_proyecto, @objetivo_proyecto, @estado_proyecto)", cn.Conectar());

                Insert.CommandType = CommandType.Text;

         
                Insert.Parameters.Add("@idPROYECTO", SqlDbType.Int).Value = idPROYECTO;
                Insert.Parameters.Add("@SEMILLERO_idSEMILLERO", SqlDbType.Int).Value = SEMILLERO_idSEMILLERO;
                Insert.Parameters.Add("@nombre_proyecto", SqlDbType.VarChar).Value = nombre_proyecto;
                Insert.Parameters.Add("@FECHA_INICIO", SqlDbType.Date).Value = FECHA_INICIO;
                Insert.Parameters.Add("@fecha_final_proyecto", SqlDbType.Date).Value = fecha_final_proyecto;
                Insert.Parameters.Add("@objetivo_proyecto", SqlDbType.VarChar).Value = objetivo_proyecto;
                Insert.Parameters.AddWithValue("@estado_proyecto", estado);

                Insert.ExecuteNonQuery();
                MessageBox.Show("Proyecto registrado exitosamente", "INSERCION PROYECTOS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cn.Cerrar();
            }
        }

        public void LlenarComboSemilleros()
        {
            try
            {
                // Consulta para traer el ID y el Nombre
                string query = "SELECT idSEMILLERO, nombre_semillero FROM SEMILLERO";
                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Configuramos el ComboBox
                comboBoxSemilleros.DataSource = dt;
                comboBoxSemilleros.DisplayMember = "nombre_semillero"; // Muestra el nombre
                comboBoxSemilleros.ValueMember = "idSEMILLERO";      // Guarda el ID oculto

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar semilleros: " + ex.Message);
                cn.Cerrar();
            }
        }

        public void VincularProyectoConInvestigador(int idP, int idI)
        {
            try
            {
                // Esta es la inserción en tu tabla 'ProyectoInvestigadores'
                SqlCommand Insert = new SqlCommand("INSERT INTO PROYECTO_INVESTIGADOR (idPROYECTO, idINVESTIGADOR) VALUES (@idP, @idI)", cn.Conectar());
                Insert.Parameters.AddWithValue("@idP", idP);
                Insert.Parameters.AddWithValue("@idI", idI);
                Insert.ExecuteNonQuery();
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el vínculo en la tabla puente: " + ex.Message);
                cn.Cerrar();
            }
        }

        public void LlenarComboLideres()
        {
            try
            {
                // Filtramos por tipo_inv para traer solo a los investigadores de tipo lider
                string query = "SELECT idInv, nombre_inv + ' ' + apellido_inv AS nombre FROM INVESTIGADOR WHERE tipo_inv = 'Lider' "; 
                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBoxLider.DataSource = dt;
                comboBoxLider.DisplayMember = "nombre";
                comboBoxLider.ValueMember = "idInv";
                cn.Cerrar();
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar líderes: " + ex.Message); cn.Cerrar(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar el registro del proyecto?", "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
