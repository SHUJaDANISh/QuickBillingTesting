using QuickBiilingTesting.Data.Repository;
using QuickBiilingTesting.Models.Dto;
using QuickBiilingTesting.Models.Entities;
using QuickBiilingTesting.Services.Interfaces;

namespace QuickBiilingTesting.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<int> CreateProduct(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            };

            return await _productRepository.CreateProduct(product);
        }

        public async Task<int> UpdateProduct(int id, ProductDto productDto)
        {
            var product = new Product
            {
                Id = id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            };

            return await _productRepository.UpdateProduct(product);
        }

        public async Task<int> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }
    }
}
