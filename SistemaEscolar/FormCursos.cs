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
    public partial class FormCursos : Form
    {
        public FormCursos()
        {
            InitializeComponent();
        }

        private void FormCursos_Load(object sender, EventArgs e)
        {
            dgvCursos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCursos.MultiSelect = false;
            dgvCursos.ReadOnly = true;
            dgvCursos.AllowUserToAddRows = false;
            dgvCursos.AllowUserToDeleteRows = false;
            dgvCursos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCursos.RowHeadersVisible = false;

            CargarProfesores();
            CargarCursos();
        }

        private void CargarProfesores()
        {
            DataTable tabla = new ConexionBD().EjecutarConsulta("SELECT IdProfesor, Nombre FROM Profesores");
            cmbProfesor.DataSource = tabla;
            cmbProfesor.DisplayMember = "Nombre";
            cmbProfesor.ValueMember = "IdProfesor";
        }

        private void CargarCursos()
        {
            string sql = @"SELECT C.IdCurso, C.NombreCurso, P.Nombre AS Profesor
                           FROM Cursos C
                           INNER JOIN Profesores P ON C.IdProfesor = P.IdProfesor";
            dgvCursos.DataSource = new ConexionBD().EjecutarConsulta(sql);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombreCurso = txtNombreCurso.Text.Trim();
            int idProfesor = Convert.ToInt32(cmbProfesor.SelectedValue);

            if (string.IsNullOrEmpty(nombreCurso))
            {
                MessageBox.Show("El nombre del curso es obligatorio.");
                return;
            }

            string consulta = $"SELECT COUNT(*) FROM Cursos WHERE NombreCurso = '{nombreCurso}' AND IdProfesor = {idProfesor}";
            DataTable resultado = new ConexionBD().EjecutarConsulta(consulta);

            if (Convert.ToInt32(resultado.Rows[0][0]) > 0)
            {
                MessageBox.Show("Ese curso ya está asignado a ese profesor.");
                return;
            }

            string sql = $"INSERT INTO Cursos (NombreCurso, IdProfesor) VALUES ('{nombreCurso}', {idProfesor})";
            new ConexionBD().EjecutarComando(sql);
            MessageBox.Show("Curso agregado correctamente.");
            CargarCursos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCursos.CurrentRow != null)
            {
                int idCurso = Convert.ToInt32(dgvCursos.CurrentRow.Cells["IdCurso"].Value);
                string nombreCurso = txtNombreCurso.Text.Trim();
                int idProfesor = Convert.ToInt32(cmbProfesor.SelectedValue);

                string sql = $"UPDATE Cursos SET NombreCurso='{nombreCurso}', IdProfesor={idProfesor} WHERE IdCurso={idCurso}";
                new ConexionBD().EjecutarComando(sql);
                MessageBox.Show("Curso actualizado.");
                CargarCursos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCursos.CurrentRow != null)
            {
                int idCurso = Convert.ToInt32(dgvCursos.CurrentRow.Cells["IdCurso"].Value);
                string sql = $"DELETE FROM Cursos WHERE IdCurso={idCurso}";
                new ConexionBD().EjecutarComando(sql);
                MessageBox.Show("Curso eliminado.");
                CargarCursos();
            }
        }

        private void dgvCursos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCursos.CurrentRow != null)
            {
                txtNombreCurso.Text = dgvCursos.CurrentRow.Cells["NombreCurso"].Value.ToString();
                cmbProfesor.Text = dgvCursos.CurrentRow.Cells["Profesor"].Value.ToString();
            }
        }
    }
}
