namespace Grocery.Core.Service
{
    using System.Collections.Generic;
    using Grocery.Core.Data.Model.DAO;

    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();

        void Save();

        void UpdateProduct(Product daoProduct);
    }
}
