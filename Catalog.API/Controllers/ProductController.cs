using Demo.Application.Interfaces;
using Demo.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductModel>>> Get()
        {
            var list = await _productService.GetProductList();
            return Ok(list);
        }

        [HttpGet("{category:length(24)}", Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductByCategory(string category)
        {
            var list = await _productService.GetProductByCategory(category);
            return Ok(list);
        }

        [HttpGet("{name:length(24)}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductByName(string name)
        {
            var list = await _productService.GetProductByName(name);
            return Ok(list);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductModel>> GetProductById(int productId)
        {
            var prd = await _productService.GetProductById(productId);
            return Ok(prd);
        }


        [HttpPost("")]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] ProductModel model)
        {
            await _productService.Create(model);
            return Ok();
        }

        [HttpPost("{models}", Name = "CreateProducts")]
        [ProducesResponseType(typeof(IEnumerable<int>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateProducts([FromBody] IEnumerable<ProductModel> models)
        {
            var prds = await _productService.CreateProducts(models);
            return Ok(prds);
        }

        [HttpPut("{model}", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct([FromBody] ProductModel model)
        {
            await _productService.Update(model);
            return Ok();
        }

        [HttpDelete("{model}" , Name = "DeleteProduct")]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProduct([FromBody] ProductModel model)
        {
            await _productService.Delete(model);
            return Ok();
        }
    }
}
