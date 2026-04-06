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
    public partial class PantallaSemilleroLider : Form
    {
        // Usamos el mismo nombre de variable para mantener coherencia en todo el proyecto
        int idLiderSesion;
        conexion cn = new conexion();
        public PantallaSemilleroLider()
        {
            InitializeComponent();
        }
        public PantallaSemilleroLider(int idRecibido)
        {
            InitializeComponent();
            this.idLiderSesion = idRecibido;
        }

        private void btnInicioLider_Click(object sender, EventArgs e)
        {
            // Devolvemos el ID al menú al regresar
            MenuPrincipalLider menuPrincipalLider = new MenuPrincipalLider(this.idLiderSesion);
            menuPrincipalLider.Show();
            this.Hide();
        }

        private void btnProyectosLider_Click(object sender, EventArgs e)
        {
            ProyectosLider proyectosLider = new ProyectosLider(this.idLiderSesion);
            proyectosLider.Show();
            this.Hide();
        }

        private void btnMiembrosLider_Click(object sender, EventArgs e)
        {
            PantallaMiembrosLIder pantallaMiembrosLIder = new PantallaMiembrosLIder(this.idLiderSesion);
            pantallaMiembrosLIder.Show();
            this.Hide();
        }

        private void btnSalirLider_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Cerrar Sesión?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void ConfigurarEdicion(bool activado)
        {
            // habilitar o deshabilitar inputs
            txtNombre.Enabled = activado;
            comboBox1.Enabled = activado;
            dTPFechaSemilleroLIder.Enabled = activado;
            txtboxDescripcion.Enabled = activado;
            txtAreadeconocimientoLider.Enabled = activado;
            dTPFechaSemilleroLIder.Enabled = false; // la fecha de creacion no deberia cambiar

            // cambiar el texto del botón para guiar al usuario
            if (activado)
            {
                btnEditarLider.Text = "Guardar Cambios";
                btnEditarLider.BackColor = Color.FromArgb(0, 192, 0); // verde para indicar acción de guardado
            }
            else
            {
                btnEditarLider.Text = "Actualizar Datos";
                btnEditarLider.BackColor = Color.FromArgb(0, 122, 204); // azul para indicar modo lectura
            }
        }
        public void CargasrDatos()
        {
            try
            {
                // consulta para traer los datos del semillero del líder logueado
                SqlConnection conexionAbierta = cn.Conectar();
                string query = "SELECT nombre_semillero, estado_semillero, fecha_creacion, area_conocimiento FROM SEMILLERO WHERE idInv = @idLider";
                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@idLider", idLiderSesion);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // si se encuentra el semillero asociado al líder
                {
                    string nombreEncontrado = reader["nombre_semillero"].ToString();
                    DateTime fecha = Convert.ToDateTime(reader["fecha_creacion"]);
                    string AreaConocimiento = reader["area_conocimiento"].ToString();
                    label10.Text = nombreEncontrado.ToUpper(); // Título principal
                    label13.Text = fecha.ToShortDateString();  // Fecha de creación visible
                    txtAreadeconocimientoLider.Text = AreaConocimiento; // Área de conocimiento visible
                    txtNombre.Text = nombreEncontrado;
                    comboBox1.Text = reader["estado_semillero"].ToString();
                    dTPFechaSemilleroLIder.Value = fecha;
                }
                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }
        private void dTPFechar_ValueChanged(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void btnInicio_Click(object sender, EventArgs e) { btnInicioLider_Click(sender, e); }
        private void btnProyectos_Click(object sender, EventArgs e) { btnProyectosLider_Click(sender, e); }

        private void PantallaSemilleroLider_Load(object sender, EventArgs e)
        {
            CargasrDatos();
            ConfigurarEdicion(false);
        }
        private void GuardarCambiosBD()
        {
            try
            {
                SqlConnection conexionAbierta = cn.Conectar();
                string query = "UPDATE SEMILLERO SET nombre_semillero = @nombre_semillero, estado_semillero = @estado_semillero, area_conocimiento = @area_conocimiento WHERE idInv = @idLider";

                SqlCommand cmd = new SqlCommand(query, conexionAbierta);
                cmd.Parameters.AddWithValue("@nombre_semillero", txtNombre.Text);
                cmd.Parameters.AddWithValue("@estado_semillero", comboBox1.Text);
                cmd.Parameters.AddWithValue("@area_conocimiento", txtAreadeconocimientoLider.Text);
                cmd.Parameters.AddWithValue("@idLider", idLiderSesion);

                cmd.ExecuteNonQuery();
                cn.Cerrar();

                // Actualizamos los labels externos con los nuevos valores de los inputs
                label10.Text = txtNombre.Text.ToUpper();
                label13.Text = dTPFechaSemilleroLIder.Value.ToShortDateString();

                MessageBox.Show("Datos del semillero actualizados correctamente.");
                ConfigurarEdicion(false); // Bloqueamos edición nuevamente
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
                if (cn.Conectar().State == ConnectionState.Open) cn.Cerrar();
            }
        }
        private void btnEditarLider_Click(object sender, EventArgs e)
        {
            // Verificamos si el nombre está deshabilitado (está en modo lectura)
            if (txtNombre.Enabled == false && txtAreadeconocimientoLider.Enabled == false)
            {
                // PASO 1: Habilitar para que el usuario pueda escribir
                ConfigurarEdicion(true);
            }
            else
            {
                // PASO 2: Como ya estaba habilitado, el usuario ya terminó de editar. 
                // Preguntamos si está seguro de guardar.
                if (MessageBox.Show("¿Desea guardar los cambios en el semillero?", "DSIG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    GuardarCambiosBD(); // Aquí es donde llamas al método que hace el UPDATE en SQL
                }
                else
                {
                    CargasrDatos(); // Recargamos datos originales
                    ConfigurarEdicion(false);
                }
            }
        }
    }
}