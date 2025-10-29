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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            ConexionBD conexion = new ConexionBD();
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            string consulta = $"SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = '{usuario}' AND Contraseña = '{contraseña}'";
            DataTable resultado = conexion.EjecutarConsulta(consulta);

            if (Convert.ToInt32(resultado.Rows[0][0]) > 0)
            {
                MenuPrincipal menu = new MenuPrincipal();
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = !chkMostrar.Checked;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
          
        }

     
    }
}
