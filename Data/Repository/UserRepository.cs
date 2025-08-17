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
    }
}
