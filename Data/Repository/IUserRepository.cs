using QuickBiilingTesting.Models.Entities;

namespace QuickBiilingTesting.Data.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
        Task<int> RegisterUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<int> DeleteUser(int id);
        Task<int> UpdateUser(User user);
        Task<IEnumerable<User>> GetAdmins();
        Task<User> GetAdminById(int id);
        Task<int> CreateAdmin(User admin);
        Task<int> UpdateAdmin(User admin);
        Task<int> DeleteAdmin(int id);


    }
}
