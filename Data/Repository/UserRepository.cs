using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Usuario.Api.Entity;

namespace Usuario.Api.Data.Repository
{
    public class UserRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        private readonly MyContext _context;

        public UserRepository(DbConnectionFactory connectionFactory, MyContext context)
        {
            _connectionFactory = connectionFactory;
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var users = await _context.Users
                    .OrderBy(u => u.Id) // Ordenar por uma coluna existente
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        
                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<User> GetByIdAsync(Guid id)
        {
            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM Users WHERE Id = @Id", new { Id = id });
                return result;
            }
        }

        public async Task InsertAsync(User user)
        {
            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                string sql = "INSERT INTO Users (Id, Name) VALUES (@Id, @Name)";
                await connection.ExecuteAsync(sql, new { Id = user.Id, Name = user.Name });
            }
        }
    }
}