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
    public partial class FormInscripciones : Form
    {
        public FormInscripciones()
        {
            InitializeComponent();
        }

        private void FormInscripciones_Load(object sender, EventArgs e)
        {
            CargarInscripciones();
            CargarEstudiantes();
            CargarCursos();
        }

        private void CargarInscripciones()
        {
            string sql = @"SELECT I.IdInscripcion, E.Nombre AS Estudiante, C.NombreCurso, I.FechaInscripcion
                   FROM Inscripciones I
                   INNER JOIN Estudiantes E ON I.IdEstudiante = E.IdEstudiante
                   INNER JOIN Cursos C ON I.IdCurso = C.IdCurso";
            dgvInscripciones.DataSource = new ConexionBD().EjecutarConsulta(sql);
        }

        private void CargarEstudiantes()
        {
            DataTable tabla = new ConexionBD().EjecutarConsulta("SELECT IdEstudiante, Nombre FROM Estudiantes");
            cmbEstudiante.DataSource = tabla;
            cmbEstudiante.DisplayMember = "Nombre";
            cmbEstudiante.ValueMember = "IdEstudiante";
        }

        private void CargarCursos()
        {
            DataTable tabla = new ConexionBD().EjecutarConsulta("SELECT IdCurso, NombreCurso FROM Cursos");
            cmbCurso.DataSource = tabla;
            cmbCurso.DisplayMember = "NombreCurso";
            cmbCurso.ValueMember = "IdCurso";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int idEstudiante = Convert.ToInt32(cmbEstudiante.SelectedValue);
            int idCurso = Convert.ToInt32(cmbCurso.SelectedValue);
            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");

            // Verificar si ya existe la inscripción
            string consulta = $"SELECT COUNT(*) FROM Inscripciones WHERE IdEstudiante = {idEstudiante} AND IdCurso = {idCurso}";
            DataTable resultado = new ConexionBD().EjecutarConsulta(consulta);

            if (Convert.ToInt32(resultado.Rows[0][0]) > 0)
            {
                MessageBox.Show("Este estudiante ya está inscrito en ese curso.");
                return;
            }

            // Insertar si no existe
            string sql = $"INSERT INTO Inscripciones (IdEstudiante, IdCurso, FechaInscripcion) VALUES ({idEstudiante}, {idCurso}, '{fecha}')";
            new ConexionBD().EjecutarComando(sql);
            MessageBox.Show("Inscripción agregada correctamente.");
            CargarInscripciones();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int idEstudiante = Convert.ToInt32(cmbEstudiante.SelectedValue);
            int idCurso = Convert.ToInt32(cmbCurso.SelectedValue);

            // Verificar si existe la inscripción
            string consulta = $"SELECT COUNT(*) FROM Inscripciones WHERE IdEstudiante = {idEstudiante} AND IdCurso = {idCurso}";
            DataTable resultado = new ConexionBD().EjecutarConsulta(consulta);

            if (Convert.ToInt32(resultado.Rows[0][0]) == 0)
            {
                MessageBox.Show("No existe una inscripción con ese estudiante y curso.");
                return;
            }

            // Eliminar si existe
            string sql = $"DELETE FROM Inscripciones WHERE IdEstudiante = {idEstudiante} AND IdCurso = {idCurso}";
            new ConexionBD().EjecutarComando(sql);
            MessageBox.Show("Inscripción eliminada correctamente.");
            CargarInscripciones();
        }

        private void dgvInscripciones_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInscripciones.CurrentRow != null)
            {
                cmbEstudiante.Text = dgvInscripciones.CurrentRow.Cells["Estudiante"].Value.ToString();
                cmbCurso.Text = dgvInscripciones.CurrentRow.Cells["NombreCurso"].Value.ToString();
                dtpFecha.Value = Convert.ToDateTime(dgvInscripciones.CurrentRow.Cells["FechaInscripcion"].Value);
            }
        }
    }
}
