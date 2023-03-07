using CRUDOperationWithQuery.Models;

namespace CRUDOperationWithQuery.Service
{
    public interface IProductService
    {
        void AddProduct(Product product);
        ICollection<Product> GetAllProducts();
        Product GetProductById(int id);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
