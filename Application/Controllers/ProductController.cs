using Domain.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Pagination;

namespace Project.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(
            ILogger<ProductController> logger, 
            IProductService service, 
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        // GET: ProductController
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] Parameters parameters)
        {
            try
            {
                var products = await _service.GetProducts(parameters);
                var productsVM = _mapper.Map<List<ProductViewModel>>(products);

                return Ok(productsVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao obter os produtos");
                return StatusCode(500, ex.Message);
            }

        }

        // GET: ProductController/
        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var data = await _service.GetProduct(id);

                if (data is null)
                    return NotFound();

                var productsVM = _mapper.Map<ProductViewModel>(data);

                return Ok(productsVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Um erro ocorreu ao obter o produto");
                return StatusCode(500, ex.Message);
            }
        }

        // POST: ProductController/
        [HttpPost]
        public async Task<IActionResult> PostAsync(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(model);

                    var data = await _service.Create(product);
                    return new CreatedAtRouteResult("GetProductById",
                        new { id = data.Id }, data);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Um erro ocorreu ao adicionar o produto");
                    ModelState.AddModelError("Erro", $"Um erro ocorreu ao adicionar o produto- " + ex.Message);
                }
            }
            return Ok(model);
        }


        // Put: ProductController/
        [HttpPut]
        public async Task<IActionResult> PutAsync(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {                   
                    var product = _mapper.Map<Product>(model);
                    await _service.Update(product);
                    return new CreatedAtRouteResult("GetProductById",
                        new { id = model.Id }, model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Um erro ocorreu ao atualizar o produto");
                    ModelState.AddModelError("Erro", $"Um erro ocorreu ao atualizar o produto- " + ex.Message);
                }
            }
            return Ok(model);
        }

        // Delete: ProductController/
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao deletar o produto");
                return Ok($"Erro: {ex.Message}");
            }
        }
    }
}
