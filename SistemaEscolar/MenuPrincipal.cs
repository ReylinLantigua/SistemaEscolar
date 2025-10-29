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
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void AbrirFormulario(Form formulario)
        {
            panelContenedor.Controls.Clear();
            formulario.TopLevel = false;
            formulario.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(formulario);
            formulario.Show();
        }

        private void estudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormEstudiantes());
        }

        private void inscripcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormInscripciones());
        }

        private void profesoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //AbrirFormulario(new FormProfesores());
        }

        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AbrirFormulario(new FormCursos());
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
