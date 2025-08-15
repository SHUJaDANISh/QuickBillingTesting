using QuickBiilingTesting.Models.Dto;
using QuickBiilingTesting.Models.Entities;

namespace QuickBiilingTesting.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<int> CreateProduct(ProductDto productDto);
        Task<int> UpdateProduct(int id, ProductDto productDto);
        Task<int> DeleteProduct(int id);
    }
}
