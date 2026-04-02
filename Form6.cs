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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtidPROYECTO.Text) || string.IsNullOrEmpty(txtNombreProyecto.Text) || string.IsNullOrEmpty(txtDescripcionProyecto.Text) || comboBoxSemilleros.SelectedItem == null || comboBoxEstadoProyecto.SelectedItem == null || !dateTimePickerInicio.Checked || !dateTimePickerFinal.Checked)
            {
                MessageBox.Show("Debe Llenar Todos los Campos Antes de Insertar Registros", "INSERCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                InsertarProyectos(int.Parse(txtidPROYECTO.Text), txtNombreProyecto.Text,  comboBoxSemilleros.SelectedItem.ToString(), comboBoxEstadoProyecto.SelectedItem.ToString(), dateTimePickerInicio.Value, dateTimePickerFinal.Value, txtDescripcionProyecto.Text); // Insertar un nuevo registro utilizando los valores ingresados en los TextBox y ComboBox

            }
            this.Hide();
        }


        public void InsertarProyectos(int idPROYECTO, int SEMILLERO_idSEMILLERO, string nombre_proyecto, DateTime FECHA_INICIO, DateTime fecha_final_proyecto, string objetivo_proyecto)
        {
            SqlCommand Insert;

            try
            {
                Insert = new SqlCommand("INSERT INTO PROYECTO (idPROYECTO, SEMILLERO_idSEMILLERO, nombre_proyecto, FECHA_INICIO, fecha_final_proyecto, objetivo_proyecto) VALUES (@idPROYECTO, @SEMILLERO_idSEMILLERO, @nombre_proyecto, @FECHA_INICIO, @fecha_final_proyecto, @objetivo_proyecto)", cn.Conectar()); // Comando SQL para ejecutar la consulta
                Insert.CommandType = CommandType.Text; // Consulta SQL de tipo TEXTO
                Insert.Parameters.AddWithValue("@idPROYECTO", SqlDbType.Int).Value = idPROYECTO;
                Insert.Parameters.AddWithValue("@SEMILLERO_idSEMILLERO", SqlDbType.Int).Value = SEMILLERO_idSEMILLERO;
                Insert.Parameters.AddWithValue("@nombre_proyecto", SqlDbType.NVarChar).Value = nombre_proyecto;
                Insert.Parameters.AddWithValue("@FECHA_INICIO", SqlDbType.DateTime).Value = FECHA_INICIO;
                Insert.Parameters.AddWithValue("@fecha_final_proyecto", SqlDbType.NVarChar).Value = fecha_final_proyecto;
                Insert.Parameters.AddWithValue("@objetivo_proyecto", SqlDbType.NVarChar).Value = objetivo_proyecto;

                Insert.ExecuteNonQuery();

                MessageBox.Show("Registro Exitoso", "INSERCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("El Proyecto Ya Existe" + "\n" + "Verifique los Datos", "INSERCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
