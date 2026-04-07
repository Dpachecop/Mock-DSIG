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
    public partial class NuevaReunionLider : Form
    {
        int idLiderSesion;
        conexion cn = new conexion();

        public NuevaReunionLider(int idRecibido)
        {
            InitializeComponent();
            this.idLiderSesion = idRecibido;
        }

        private void btnCancelarSActividad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar la creación de la reunión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnAgregarReu_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDREU.Text) || string.IsNullOrWhiteSpace(txtMotivoR.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos (ID y Motivo).", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlConnection conexionAbierta = cn.Conectar();

                // verificar si el ID ya existe para evitar duplicaciones
                string queryValidar = "SELECT COUNT(*) FROM REUNION WHERE idREUNION = @id";
                SqlCommand cmdValidar = new SqlCommand(queryValidar, conexionAbierta);
                cmdValidar.Parameters.AddWithValue("@id", txtIDREU.Text.Trim());

                int existe = (int)cmdValidar.ExecuteScalar();

                if (existe > 0)
                {
                    MessageBox.Show("El ID de reunión ya existe. Por favor, use uno diferente.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cn.Cerrar();
                    return;
                }

                // 3. INSERCIÓN: Si el ID es nuevo, procedemos a guardar
                string queryInsert = "INSERT INTO REUNION (idREUNION, SEMILLERO_idSEMILLERO, motivo_reunion, fecha_reunion) " +
                                     "SELECT @id, SEMILLERO_idSEMILLERO, @motivo_reunion, @fecha_reunion " +
                                     "FROM INVESTIGADOR WHERE idInv = @idLider";

                SqlCommand cmdInsert = new SqlCommand(queryInsert, conexionAbierta);
                cmdInsert.Parameters.AddWithValue("@id", txtIDREU.Text.Trim());
                cmdInsert.Parameters.AddWithValue("@motivo_reunion", txtMotivoR.Text.Trim());
                cmdInsert.Parameters.AddWithValue("@fecha_reunion", dtpReuFecha.Value);
                cmdInsert.Parameters.AddWithValue("@idLider", idLiderSesion);

                int filas = cmdInsert.ExecuteNonQuery();

                if (filas > 0)
                {
                    MessageBox.Show("Reunión guardada exitosamente.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo asociar la reunión a su semillero.", "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error técnico: " + ex.Message, "DSIG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
    }   }
}