using Dapper;
using QuickBiilingTesting.Models.Entities;
using System.Threading.Tasks;

namespace QuickBiilingTesting.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM sp_getuserbyusername(@Username);";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
        }

        public async Task<int> RegisterUser(User user)
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT sp_registeruser(@Username, @Password, @Email, @Role);";
            return await connection.ExecuteScalarAsync<int>(sql, new
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Role = user.Role
            });
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT Id, Username, Email, Role, CreatedAt FROM Users ORDER BY CreatedAt DESC;";
            return await connection.QueryAsync<User>(sql);
        }
        public async Task<User> GetUserById(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM Users WHERE Id = @Id;";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<int> DeleteUser(int id)
        {
            using var conn = _context.CreateConnection();
            var sql = "DELETE FROM Users WHERE Id = @Id AND Role = 'User';";
            return await conn.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<int> UpdateUser(User user)
        {
            using var conn = _context.CreateConnection();
            var sql = @"UPDATE Users SET Username = @Username, Email = @Email 
                WHERE Id = @Id AND Role = 'User';";
            return await conn.ExecuteAsync(sql, user);
        }

        public async Task<IEnumerable<User>> GetAdmins()
        {
            using var conn = _context.CreateConnection();
            var sql = "SELECT Id, Username, Email, Role, CreatedAt FROM Users WHERE Role = 'Admin';";
            return await conn.QueryAsync<User>(sql);
        }

        public async Task<User> GetAdminById(int id)
        {
            using var conn = _context.CreateConnection();
            var sql = "SELECT Id, Username, Email, Role, CreatedAt FROM Users WHERE Id = @Id AND Role = 'Admin';";
            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<int> CreateAdmin(User admin)
        {
            using var conn = _context.CreateConnection();
            var sql = @"INSERT INTO Users (Username, Password, Email, Role)
                VALUES (@Username, @Password, @Email, 'Admin') RETURNING Id;";
            return await conn.ExecuteScalarAsync<int>(sql, admin);
        }

        public async Task<int> UpdateAdmin(User admin)
        {
            using var conn = _context.CreateConnection();
            var sql = @"UPDATE Users SET Username = @Username, Email = @Email 
                WHERE Id = @Id AND Role = 'Admin';";
            return await conn.ExecuteAsync(sql, admin);
        }

        public async Task<int> DeleteAdmin(int id)
        {
            using var conn = _context.CreateConnection();
            var sql = "DELETE FROM Users WHERE Id = @Id AND Role = 'Admin';";
            return await conn.ExecuteAsync(sql, new { Id = id });
        }

    }
}
