using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Data;
using UsuarioAPI.Models;

namespace UsuarioAPI.Repositories
{
    public class UsusarioSpRepository
    {
        private readonly AppDbContext _context;


        public UsusarioSpRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetUsuariosFromSP()
        {
            return await _context.Usuarios
                .FromSqlRaw("EXEC SP_GetAllUsuarios")
                .ToListAsync();
        }

        public async Task<Usuario> GetUsuariosFromIdFromSP(int id)
        {
            var idP = new SqlParameter("@Id", id);
            var usuario = await _context.Usuarios
                .FromSqlRaw("EXEC SP_GetUsuarioById @Id", idP)
                .ToListAsync();
            return usuario.FirstOrDefault();
        }

        public async Task<int> CreateUsuarioFromSP(Usuario usuario)
        {
            var parametros = new[]
            {
                new SqlParameter("@Firstname", usuario.Firstname),
                new SqlParameter("@Secondname", usuario.Secondname),
                new SqlParameter("@Mail", usuario.Mail),
                new SqlParameter("@Salary", usuario.Salary),
                new SqlParameter("@Numberx", usuario.Numberx)
            };

            return await _context.Database.ExecuteSqlRawAsync("EXEC SP_CreateUsuario @Firstname, @Secondname, @Mail, @Salary, @Numberx", parametros);
        }

        public async Task<int> UpdateUsuarioFromSP(Usuario usuario)
        {
            var parametros = new[]
            {
                new SqlParameter("@Id", usuario.Id),
                new SqlParameter("@Firstname", usuario.Firstname),
                new SqlParameter("@Secondname", usuario.Secondname),
                new SqlParameter("@Mail", usuario.Mail),
                new SqlParameter("@Salary", usuario.Salary),
                new SqlParameter("@Numberx", usuario.Numberx)
            };

            return await _context.Database.ExecuteSqlRawAsync("EXEC SP_UpdateUsuario @Id, @Firstname, @Secondname, @Mail, @Salary, @Numberx", parametros);
        }

        public async Task<int> DeleteUsuarioFromSP(int id)
        {
            var idP = new SqlParameter("@Id", id);
            return await _context.Database.ExecuteSqlRawAsync("EXEC SP_DeleteUsuario @Id", idP);
        }

    }
}
