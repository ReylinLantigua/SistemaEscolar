using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEscolar
{
    class ConexionBD
    {
        private readonly string cadenaConexion = "Data Source=.;Initial Catalog=EscuelaDB;Integrated Security=True;";
        private SqlConnection conexion;

        public ConexionBD()
        {
            conexion = new SqlConnection(cadenaConexion);
        }

        public void Abrir()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
        }

        public void Cerrar()
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
        }

        public DataTable EjecutarConsulta(string consulta)
        {
            Abrir();
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            Cerrar();
            return tabla;
        }

        public int EjecutarComando(string comandoSQL)
        {
            Abrir();
            SqlCommand comando = new SqlCommand(comandoSQL, conexion);
            int filasAfectadas = comando.ExecuteNonQuery();
            Cerrar();
            return filasAfectadas;
        }

        public int EjecutarComandoConParametros(string comandoSQL, SqlParameter[] parametros)
        {
            Abrir();
            SqlCommand comando = new SqlCommand(comandoSQL, conexion);
            comando.Parameters.AddRange(parametros);
            int filasAfectadas = comando.ExecuteNonQuery();
            Cerrar();
            return filasAfectadas;
        }
    }
}
