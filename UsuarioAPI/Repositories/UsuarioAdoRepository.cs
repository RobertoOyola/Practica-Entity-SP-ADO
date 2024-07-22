using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Data;
using UsuarioAPI.Models;

namespace UsuarioAPI.Repositories
{
    public class UsuarioAdoRepository
    {
        private readonly string _connectionString;

        public UsuarioAdoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Usuario> GetAllUsuariosAdo()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM usuario", connection);
                command.CommandType = CommandType.Text;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            Id = reader.GetInt32(0),
                            Firstname = reader.GetString(1),
                            Secondname = reader.GetString(2),
                            Mail = reader.GetString(3),
                            Salary = reader.GetDecimal(4),
                            Numberx = reader.GetInt32(5)
                        };
                        usuarios.Add(usuario);
                    }
                }
            }
            return usuarios;
        }

        public Usuario GetAllUsuarioIdAdo(int id)
        {
            Usuario usuario = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM usuario WHERE id = @Id", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = reader.GetInt32(0),
                            Firstname = reader.GetString(1),
                            Secondname = reader.GetString(2),
                            Mail = reader.GetString(3),
                            Salary = reader.GetDecimal(4),
                            Numberx = reader.GetInt32(5)
                        };
                    }
                }
            }
            return usuario;
        }

        public void InsertUsuarioAdo(Usuario usuario)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO usuario (firstname, secondname, mail, salary, numberx) VALUES (@firstname, @secondname, @mail, @salary, @numberx)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@firstname", usuario.Firstname);
                    command.Parameters.AddWithValue("@secondname", usuario.Secondname);
                    command.Parameters.AddWithValue("@mail", usuario.Mail);
                    command.Parameters.AddWithValue("@salary", usuario.Salary);
                    command.Parameters.AddWithValue("@numberx", usuario.Numberx);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateUsuarioAdo(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Consulta SQL para actualizar el usuario
                string query = "UPDATE usuario SET firstname = @firstname, secondname = @secondname, mail = @mail, salary = @salary, numberx = @numberx WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;

                    // Agregar parámetros
                    command.Parameters.AddWithValue("@firstname", usuario.Firstname);
                    command.Parameters.AddWithValue("@secondname", usuario.Secondname);
                    command.Parameters.AddWithValue("@mail", usuario.Mail);
                    command.Parameters.AddWithValue("@salary", usuario.Salary);
                    command.Parameters.AddWithValue("@numberx", usuario.Numberx);
                    command.Parameters.AddWithValue("@id", usuario.Id);

                    connection.Open();

                    // Ejecutar el comando
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró el usuario para actualizar.");
                    }
                }
            }
        }

        public void DeleteUsuarioAdo(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM usuario WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró el usuario para eliminar.");
                    }
                }
            }

        }
    }
}
