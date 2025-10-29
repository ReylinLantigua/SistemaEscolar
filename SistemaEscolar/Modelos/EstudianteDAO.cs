using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEscolar.Modelos
{
    class EstudianteDAO
    {
        private ConexionBD conexion = new ConexionBD();

        public List<Estudiante> ObtenerTodos()
        {
            List<Estudiante> lista = new List<Estudiante>();
            DataTable tabla = conexion.EjecutarConsulta("SELECT * FROM Estudiantes");

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new Estudiante
                {
                    IdEstudiante = Convert.ToInt32(fila["IdEstudiante"]),
                    Nombre = fila["Nombre"].ToString(),
                    Matricula = fila["Matricula"].ToString(),
                    FechaNacimiento = Convert.ToDateTime(fila["FechaNacimiento"]),
                    Curso = fila["Curso"].ToString()
                });
            }

            return lista;
        }

        public void Insertar(Estudiante est)
        {
            string sql = "INSERT INTO Estudiantes (Nombre, Matricula, FechaNacimiento, Curso) VALUES (@nombre, @matricula, @fecha, @curso)";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", est.Nombre),
                new SqlParameter("@matricula", est.Matricula),
                new SqlParameter("@fecha", est.FechaNacimiento),
                new SqlParameter("@curso", est.Curso)
            };
            conexion.EjecutarComandoConParametros(sql, parametros);
        }

        public void Actualizar(Estudiante est)
        {
            string sql = "UPDATE Estudiantes SET Nombre=@nombre, Matricula=@matricula, FechaNacimiento=@fecha, Curso=@curso WHERE IdEstudiante=@id";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", est.Nombre),
                new SqlParameter("@matricula", est.Matricula),
                new SqlParameter("@fecha", est.FechaNacimiento),
                new SqlParameter("@curso", est.Curso),
                new SqlParameter("@id", est.IdEstudiante)
            };
            conexion.EjecutarComandoConParametros(sql, parametros);
        }

        public void Eliminar(int id)
        {
            string sql = "DELETE FROM Estudiantes WHERE IdEstudiante=@id";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            conexion.EjecutarComandoConParametros(sql, parametros);
        }
    }
}
