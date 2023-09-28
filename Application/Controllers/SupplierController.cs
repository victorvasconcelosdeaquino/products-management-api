using Domain.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.DTO;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService _service;
        private readonly IMapper _mapper;

        public SupplierController(ILogger<SupplierController> logger, ISupplierService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        // GET: SupplierController
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var products = await _service.GetSuppliers();
                var productsVM = _mapper.Map<List<SupplierViewModel>>(products);

                return Ok(productsVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro ocorreu ao obter os produtos");
                return StatusCode(500, ex.Message);
            }

        }

        // GET: SupplierController/5
        [HttpGet("{id:int}", Name = "GetSupplierById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var data = await _service.GetSupplier(id);

                if (data is null)
                    return NotFound();

                var productsVM = _mapper.Map<SupplierViewModel>(data);

                return Ok(productsVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Um erro ocorreu ao obter o fornecedor");
                return StatusCode(500, ex.Message);
            }
        }

        // POST: SupplierController/
        [HttpPost]
        public async Task<IActionResult> PostAsync(SupplierDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var supplier = _mapper.Map<Supplier>(model);

                    var data = await _service.Create(supplier);
                    return new CreatedAtRouteResult("GetSupplierById",
                        new { id = data.Id }, data);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Um erro ocorreu ao adicionar o fornecedor");
                    ModelState.AddModelError("Erro", $"Um erro ocorreu ao adicionar o fornecedor- " + ex.Message);
                    return Ok(model);
                }
            }
            return Ok(model);
        }


        // Put: SupplierController/
        [HttpPut]
        public async Task<IActionResult> PutAsync(SupplierDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var supplier = _mapper.Map<Supplier>(model);
                    await _service.Update(supplier);
                    return new CreatedAtRouteResult("GetSupplierById",
                        new { id = model.Id }, model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Um erro ocorreu ao atualizar o fornecedor");
                    ModelState.AddModelError("Erro", $"Um erro ocorreu ao atualizar o fornecedor- " + ex.Message);
                    return Ok(model);
                }
            }
            return Ok(model);
        }

        // Delete: SupplierController/5
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
                _logger.LogError(ex, "Um erro ocorreu ao deletar o fornecedor");
                return Ok($"Erro: {ex.Message}");
            }
        }
    }
}
