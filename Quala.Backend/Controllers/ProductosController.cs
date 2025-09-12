using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quala.Backend.Models;
using Quala.Backend.Repositories.Interfaces;
using Quala.Backend.Services.Interfaces;
using static Quala.Backend.Constants.AppConstants;

namespace Quala.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productoService;

    public ProductosController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetAllProducto()
    {

        try
        {
            var productos = await _productoService.GetAllProductosAsync();
            return Ok(productos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto (int id)
    {
        var producto = await _productoService.GetProductoByIdAsync(id);

        if (producto == null) 
        { 
            return NotFound();
        }
        return Ok(producto);
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto ( Producto producto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdProducto = await _productoService.CreateProductoAsync(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = createdProducto.Id }, createdProducto);
        }
        catch (InvalidOperationException ex) 
        {
            return Conflict(ex.Message); 
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }

    }


    // PUT: api/productos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducto(int id, Producto producto)
    {
        if (id != producto.Id)
        {
            return BadRequest(ErrorMessages.ValidationUpdate);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedProducto = await _productoService.UpdateProductoAsync(id, producto);
            return Ok(updatedProducto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
       try
        {
            var deleted = await _productoService.DeleteProductoAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex) 
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

}

