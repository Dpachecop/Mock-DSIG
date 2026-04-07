using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mock_DSIG
{
    public partial class EditarReuLider : Form
    {
        conexion cn = new conexion();
        int idReunion;
        public EditarReuLider(int id, string motivo, DateTime fecha)
        {
            InitializeComponent();
            this.idReunion = id;
            txtIDREU.Text = id.ToString();
            txtIDREU.Enabled = false; // el ID no se debe editar porque es la llave primaria
            txtMotivoR.Text = motivo;
            dtpReuFecha.Value = fecha;
        }

        private void btnCancelarSActividad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar la edición de la reunión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnACTREU_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMotivoR.Text))
            {
                MessageBox.Show("El motivo no puede estar vacío.");
                return;
            }

            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                string query = "UPDATE REUNION SET motivo_reunion = @motivo_reunion, fecha_reunion = @fecha_reunion WHERE idREUNION = @id";

                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@motivo_reunion", txtMotivoR.Text.Trim());
                cmd.Parameters.AddWithValue("@fecha_reunion", dtpReuFecha.Value);
                cmd.Parameters.AddWithValue("@id", idReunion);

                cmd.ExecuteNonQuery();
                cn.Cerrar();

                MessageBox.Show("Reunión actualizada correctamente.", "DSIG");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }
    }
}
