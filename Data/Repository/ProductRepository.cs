using Dapper;
using QuickBiilingTesting.Models.Entities;
using System.Data;

namespace QuickBiilingTesting.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext _context;

        public ProductRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Product>(
                "sp_GetAllProducts",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Product> GetProductById(int id)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@ProductId", id);

            return await connection.QueryFirstOrDefaultAsync<Product>(
                "sp_GetProductById",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateProduct(Product product)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Name", product.Name);
            parameters.Add("@Description", product.Description);
            parameters.Add("@Price", product.Price);
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_CreateProduct",
                parameters,
                commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@Id");
        }

        public async Task<int> UpdateProduct(Product product)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@ProductId", product.Id);
            parameters.Add("@Name", product.Name);
            parameters.Add("@Description", product.Description);
            parameters.Add("@Price", product.Price);

            return await connection.ExecuteAsync(
                "sp_UpdateProduct",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteProduct(int id)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@ProductId", id);

            return await connection.ExecuteAsync(
                "sp_DeleteProduct",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
