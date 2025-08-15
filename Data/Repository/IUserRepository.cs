using QuickBiilingTesting.Models.Entities;

namespace QuickBiilingTesting.Data.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
        Task<int> RegisterUser(User user);
    }
}
