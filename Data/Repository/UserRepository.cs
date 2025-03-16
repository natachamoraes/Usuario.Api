using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
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
                    .OrderBy(u => u.Id)
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

        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                var entity = _context.Users.FirstOrDefault(x => x.Id == id);
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> InsertAsync(User user)
        {
            try
            {
                var entity = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return entity.Entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<User> UpdateAsync(User user)
        {
            try
            {
                var findUser = await _context.Users.FindAsync(user.Id);
                if (findUser == null) throw new ArgumentException("Usuário não encontrado");

                _context.Entry(findUser).State = EntityState.Detached;

                var editUser = _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return editUser.Entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<User?> DeleteAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) throw new ArgumentException("Usuário não encontrado");
                
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}