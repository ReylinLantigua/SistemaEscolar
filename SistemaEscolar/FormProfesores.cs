using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaEscolar
{
    public partial class FormProfesores : Form
    {
        public FormProfesores()
        {
            InitializeComponent();
        }

        private void FormProfesores_Load(object sender, EventArgs e)
        {
            dgvProfesores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProfesores.MultiSelect = false;
            dgvProfesores.ReadOnly = true;
            dgvProfesores.AllowUserToAddRows = false;
            dgvProfesores.AllowUserToDeleteRows = false;
            dgvProfesores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProfesores.RowHeadersVisible = false;

            CargarProfesores();
        }

        private void CargarProfesores()
        {
            dgvProfesores.DataSource = new ConexionBD().EjecutarConsulta("SELECT * FROM Profesores");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string especialidad = txtEspecialidad.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(especialidad) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            string consulta = $"SELECT COUNT(*) FROM Profesores WHERE Email = '{email}'";
            DataTable resultado = new ConexionBD().EjecutarConsulta(consulta);

            if (Convert.ToInt32(resultado.Rows[0][0]) > 0)
            {
                MessageBox.Show("Ya existe un profesor con ese correo.");
                return;
            }

            string sql = $"INSERT INTO Profesores (Nombre, Especialidad, Email) VALUES ('{nombre}', '{especialidad}', '{email}')";
            new ConexionBD().EjecutarComando(sql);
            MessageBox.Show("Profesor agregado correctamente.");
            CargarProfesores();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProfesores.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvProfesores.CurrentRow.Cells["IdProfesor"].Value);
                string nombre = txtNombre.Text.Trim();
                string especialidad = txtEspecialidad.Text.Trim();
                string email = txtEmail.Text.Trim();

                string sql = $"UPDATE Profesores SET Nombre='{nombre}', Especialidad='{especialidad}', Email='{email}' WHERE IdProfesor={id}";
                new ConexionBD().EjecutarComando(sql);
                MessageBox.Show("Profesor actualizado.");
                CargarProfesores();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProfesores.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvProfesores.CurrentRow.Cells["IdProfesor"].Value);
                string sql = $"DELETE FROM Profesores WHERE IdProfesor={id}";
                new ConexionBD().EjecutarComando(sql);
                MessageBox.Show("Profesor eliminado.");
                CargarProfesores();
            }
        }

        private void dgvProfesores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProfesores.CurrentRow != null)
            {
                txtNombre.Text = dgvProfesores.CurrentRow.Cells["Nombre"].Value.ToString();
                txtEspecialidad.Text = dgvProfesores.CurrentRow.Cells["Especialidad"].Value.ToString();
                txtEmail.Text = dgvProfesores.CurrentRow.Cells["Email"].Value.ToString();
            }
        }
    }
}
