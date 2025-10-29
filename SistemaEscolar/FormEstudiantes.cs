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
    public partial class FormEstudiantes : Form
    {
        public FormEstudiantes()
        {
            InitializeComponent();
        }

        private void FormEstudiantes_Load(object sender, EventArgs e)
        {
            dgvEstudiantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEstudiantes.MultiSelect = false;
            dgvEstudiantes.ReadOnly = true;
            dgvEstudiantes.AllowUserToAddRows = false;
            dgvEstudiantes.AllowUserToDeleteRows = false;
            dgvEstudiantes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEstudiantes.RowHeadersVisible = false;

            CargarEstudiantes();
        }

        private void CargarEstudiantes()
        {
            ConexionBD conexion = new ConexionBD();
            dgvEstudiantes.DataSource = conexion.EjecutarConsulta("SELECT * FROM Estudiantes");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string matricula = txtMatricula.Text.Trim();
            string curso = txtCurso.Text.Trim();
            string fecha = dtpNacimiento.Value.ToString("yyyy-MM-dd");

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(matricula) || string.IsNullOrEmpty(curso))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            // Verificar si la matrícula ya existe
            string consulta = $"SELECT COUNT(*) FROM Estudiantes WHERE Matricula = '{matricula}'";
            DataTable resultado = new ConexionBD().EjecutarConsulta(consulta);

            if (Convert.ToInt32(resultado.Rows[0][0]) > 0)
            {
                MessageBox.Show("Ya existe un estudiante con esa matrícula.");
                return;
            }

            string comando = $"INSERT INTO Estudiantes (Nombre, Matricula, FechaNacimiento, Curso) VALUES ('{nombre}', '{matricula}', '{fecha}', '{curso}')";
            new ConexionBD().EjecutarComando(comando);
            MessageBox.Show("Estudiante agregado correctamente.");
            CargarEstudiantes();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEstudiantes.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvEstudiantes.CurrentRow.Cells["IdEstudiante"].Value);
                string nombre = txtNombre.Text.Trim();
                string matricula = txtMatricula.Text.Trim();
                string curso = txtCurso.Text.Trim();
                string fecha = dtpNacimiento.Value.ToString("yyyy-MM-dd");

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(matricula) || string.IsNullOrEmpty(curso))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                string comando = $"UPDATE Estudiantes SET Nombre='{nombre}', Matricula='{matricula}', FechaNacimiento='{fecha}', Curso='{curso}' WHERE IdEstudiante={id}";
                new ConexionBD().EjecutarComando(comando);
                MessageBox.Show("Estudiante actualizado.");
                CargarEstudiantes();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEstudiantes.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvEstudiantes.CurrentRow.Cells["IdEstudiante"].Value);
                string comando = $"DELETE FROM Estudiantes WHERE IdEstudiante = {id}";
                new ConexionBD().EjecutarComando(comando);
                MessageBox.Show("Estudiante eliminado.");
                CargarEstudiantes();
            }
        }

        private void dgvEstudiantes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEstudiantes.CurrentRow != null)
            {
                txtNombre.Text = dgvEstudiantes.CurrentRow.Cells["Nombre"].Value.ToString();
                txtMatricula.Text = dgvEstudiantes.CurrentRow.Cells["Matricula"].Value.ToString();
                txtCurso.Text = dgvEstudiantes.CurrentRow.Cells["Curso"].Value.ToString();
                dtpNacimiento.Value = Convert.ToDateTime(dgvEstudiantes.CurrentRow.Cells["FechaNacimiento"].Value);
            }
        }
    }
}
