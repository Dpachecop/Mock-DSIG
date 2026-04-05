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
    public partial class EditarSemillerosAdmin : Form
    {
        conexion cn = new conexion();
        int idOriginal; // variable para almacenar el ID del semillero que se va a editar

        public void VerificarLetras(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
                MessageBox.Show("Solo Puedes Ingresar Letras.", "PERSONAS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public void VerificarNumeritos(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
                MessageBox.Show("Solo Puedes Ingresar Números.", "PERSONAS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public EditarSemillerosAdmin(int id, string nombre, string area, string estado, DateTime fecha)
        {
            InitializeComponent();
            LLenarComboInvestigadores();

            // guardamos el ID para el where de la consulta
            this.idOriginal = id;
            txtidSEMILLEROEdit.Text = id.ToString();
            txtNombreSemilleroEdit.Text = nombre;
            txtAreaconocimientosemilleroEdit.Text = area;
            comboBoxEstadoSemilleroEdit.Text = estado;
            dateTimePickerInicioSemilleroEdit.Value = fecha;
        }
        public EditarSemillerosAdmin()
        {
            InitializeComponent();
        }

        private void btnGuardarSemilleroEditado_Click(object sender, EventArgs e)
        { 
            EditarSemilleros(int.Parse(txtidSEMILLEROEdit.Text),txtNombreSemilleroEdit.Text, txtAreaconocimientosemilleroEdit.Text, comboBoxEstadoSemilleroEdit.Text, dateTimePickerInicioSemilleroEdit.Value);
        }
        public void EditarSemilleros(int id, string nombre, string area, string estado, DateTime fecha)
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();

                // se identifica quien era el líder antes del cambio
                string liderviejo = "SELECT idInv FROM SEMILLERO WHERE idSEMILLERO = @id";
                SqlCommand cmdviejo = new SqlCommand(liderviejo, conexionAbierta);
                cmdviejo.Parameters.AddWithValue("@id", idOriginal);
                object resultado = cmdviejo.ExecuteScalar();

                int idAnterior = (resultado != null) ? Convert.ToInt32(resultado) : 0;
                int nuevoIdLider = Convert.ToInt32(comboBoxInvestigadorLiderEdit.SelectedValue);

                // a partir de aca se empieza a hacer el poceso para actualizar el semillero
                string Actualizacion = @"UPDATE SEMILLERO SET 
                                     nombre_semillero = @nom, 
                                     area_conocimiento = @area, 
                                     estado_semillero = @est, 
                                     fecha_creacion = @fec,
                                     idInv = @nIdLider
                                     WHERE idSEMILLERO = @id";

                SqlCommand act = new SqlCommand(Actualizacion, conexionAbierta);
                act.Parameters.AddWithValue("@nom", nombre);
                act.Parameters.AddWithValue("@area", area);
                act.Parameters.AddWithValue("@est", estado);
                act.Parameters.AddWithValue("@fec", fecha);
                act.Parameters.AddWithValue("@nIdLider", nuevoIdLider);
                act.Parameters.AddWithValue("@id", idOriginal);
                act.ExecuteNonQuery();

                if (idAnterior != 0 && idAnterior != nuevoIdLider)
                {
                    // el anterior vuelve a ser Investigador
                    new SqlCommand($"UPDATE INVESTIGADOR SET tipo_inv = 'INVESTIGADOR' WHERE idInv = {idAnterior}", conexionAbierta).ExecuteNonQuery();
                    // el nuevo sube a Lider
                    new SqlCommand($"UPDATE INVESTIGADOR SET tipo_inv = 'LÍDER' WHERE idInv = {nuevoIdLider}", conexionAbierta).ExecuteNonQuery();
                }

                MessageBox.Show("Cambios guardados con éxito.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.Cerrar();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar datos: " + ex.Message);
                cn.Cerrar();
            }
        }

        public void LLenarComboInvestigadores()
        {
            try
            {
                string query = "SELECT idInv, nombre_inv + ' ' + apellido_inv AS nombre_completo FROM INVESTIGADOR";
                SqlDataAdapter da = new SqlDataAdapter(query, cn.Conectar());
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBoxInvestigadorLiderEdit.DataSource = dt;
                comboBoxInvestigadorLiderEdit.DisplayMember = "nombre_completo";
                comboBoxInvestigadorLiderEdit.ValueMember = "idInv";
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar líderes: " + ex.Message);
                cn.Cerrar();
            }
        }

        private void btnCancelarEdit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelarEdit_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar la edición? Se perderán los cambios no guardados.", "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
        }   }

        private void txtidSEMILLEROEdit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreSemilleroEdit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreSemilleroEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void txtAreaconocimientosemilleroEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            VerificarLetras(e);
        }
    }
}