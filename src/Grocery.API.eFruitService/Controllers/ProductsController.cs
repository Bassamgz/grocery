namespace Grocery.API.eFruitService.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Grocery.Core.Data.Model.DTO;
    using Grocery.Core.Service;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        // GET api/products
        [AcceptVerbs("GET")]
        [Route("")]
        public IActionResult GetAllProducts()
        {
            var products = productService.GetAllProducts();
            if (products == null)
            {
                return NotFound();
            }

            var dtoProducts = mapper.Map<IEnumerable<Core.Data.Model.DAO.Product>, IEnumerable<Product>>(products);
            return Ok(dtoProducts);
        }

        // PUT api/products/
        [AcceptVerbs("PUT")]
        [Route("")]
        public IActionResult PutProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var daoProduct = Mapper.Map<Core.Data.Model.DAO.Product>(product);

            productService.UpdateProduct(daoProduct);

            return NoContent();
        }
    }
}