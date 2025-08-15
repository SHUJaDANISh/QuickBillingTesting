using Dapper;
using QuickBiilingTesting.Models.Entities;
using System.Data;

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
            var parameters = new DynamicParameters();
            parameters.Add("@Username", username);

            return await connection.QueryFirstOrDefaultAsync<User>(
                "sp_GetUserByUsername",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        //public async Task<int> RegisterUser(User user)
        //{
        //    using var connection = _context.CreateConnection();
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@Username", user.Username);
        //    parameters.Add("@Password", user.Password);
        //    parameters.Add("@Email", user.Email);
        //    parameters.Add("@Role", user.Role);
        //    parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        //    await connection.ExecuteAsync(
        //        "sp_RegisterUser",
        //        parameters,
        //        commandType: CommandType.StoredProcedure);

        //    return parameters.Get<int>("@Id");
        //}
        public async Task<int> RegisterUser(User user)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Username", user.Username);
            parameters.Add("@Password", user.Password);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Role", user.Role);

            // If your stored procedure doesn't have an output parameter
            return await connection.ExecuteScalarAsync<int>(
                "sp_RegisterUser",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
