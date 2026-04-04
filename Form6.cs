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
    public partial class Form6 : Form
    {
        // Instanciación de Clases
        conexion cn = new conexion();

        public Form6()
        {
            InitializeComponent();
            LlenarComboSemilleros(); // Cargar semilleros al iniciar
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtidPROYECTO.Text) || string.IsNullOrEmpty(txtNombreProyecto.Text) || string.IsNullOrEmpty(txtDescripcionProyecto.Text) || comboBoxSemilleros.SelectedValue == null)
            {
                MessageBox.Show("Faltan datos obligatorios (ID, Nombre y Semillero).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idProy = int.Parse(txtidPROYECTO.Text);
                int idSemi = Convert.ToInt32(comboBoxSemilleros.SelectedValue);
                string nombre = txtNombreProyecto.Text;
                DateTime inicio = dateTimePickerInicio.Value;
                DateTime final = dateTimePickerFinal.Value;
                string objetivo = txtDescripcionProyecto.Text;
                string estadoSeleccionado = comboBoxEstadoProyecto.SelectedItem != null ? comboBoxEstadoProyecto.SelectedItem.ToString() : "Activo";
                InsertarProyectos(idProy, idSemi, nombre, inicio, final, objetivo, estadoSeleccionado);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el proceso de captura: " + ex.Message);
            }
        }

        public void InsertarProyectos(int idPROYECTO, int SEMILLERO_idSEMILLERO, string nombre_proyecto, DateTime FECHA_INICIO, DateTime fecha_final_proyecto, string objetivo_proyecto, string estado)
        {
            try
            {
                string query = @"INSERT INTO PROYECTO (idPROYECTO, SEMILLERO_idSEMILLERO, nombre_proyecto, FECHA_INICIO, fecha_final_proyecto, objetivo_proyecto, estado_proyecto) 
                                 VALUES (@id, @idSemi, @nom, @fInicio, @fFinal, @obj, @est)";

                SqlCommand cmd = new SqlCommand(query, cn.Conectar());

                cmd.Parameters.AddWithValue("@id", idPROYECTO);
                cmd.Parameters.AddWithValue("@idSemi", SEMILLERO_idSEMILLERO);
                cmd.Parameters.AddWithValue("@nom", nombre_proyecto);
                cmd.Parameters.AddWithValue("@fInicio", FECHA_INICIO);
                cmd.Parameters.AddWithValue("@fFinal", fecha_final_proyecto);
                cmd.Parameters.AddWithValue("@obj", objetivo_proyecto);
                cmd.Parameters.AddWithValue("@est", estado);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Proyecto registrado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar en la base de datos: " + ex.Message, "ERROR SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cn.Cerrar();
            }
        }

        public void LlenarComboSemilleros()
        {
            try
            {
                string query = "SELECT idSEMILLERO, nombre_semillero FROM SEMILLERO";
                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBoxSemilleros.DataSource = dt;
                comboBoxSemilleros.DisplayMember = "nombre_semillero";
                comboBoxSemilleros.ValueMember = "idSEMILLERO";

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ComboBox de semilleros: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}