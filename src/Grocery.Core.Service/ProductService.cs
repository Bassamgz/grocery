namespace Grocery.Core.Service
{
    using System;
    using System.Collections.Generic;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;
    using Grocery.Core.Data.Repositories;

    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = productRepository.GetAll();
            return products;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void UpdateProduct(Product daoProduct)
        {
            var product = productRepository.GetById(daoProduct.Id);
            if (product != null)
            {
                product.Name = daoProduct.Name;
                product.Price = daoProduct.Price;
                product.UpdatedOn = DateTime.Now;
                productRepository.Update(product);
            }
            else
            {
                productRepository.Add(new Product { Name = daoProduct.Name, Price = daoProduct.Price });
            }

            Save();
        }
    }
}
