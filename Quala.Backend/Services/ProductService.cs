using Quala.Backend.Models;
using Quala.Backend.Repositories.Interfaces;
using Quala.Backend.Services.Interfaces;

namespace Quala.Backend.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productoRepository;

    public ProductoService(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<IEnumerable<Producto>> GetAllProductosAsync()
    {
        return await _productoRepository.GetAllAsync();
    }

    public async Task<Producto> GetProductoByIdAsync(int id)
    {
        return await _productoRepository.GetByIdAsync(id);
    }

    public async Task<Producto> CreateProductoAsync(Producto producto)
    {
        var existingProducto = await _productoRepository.GetByCodigoAsync(producto.CodigoProducto);

        if (existingProducto != null)
        {
            throw new InvalidOperationException($"Ya existe un producto con el código {producto.CodigoProducto}");
        }

        if (producto.PrecioUnitario < 0)
            throw new ArgumentException("El precio unitario no puede ser negativo");

        if (string.IsNullOrEmpty(producto.Nombre))
            throw new ArgumentException("El nombre del producto es requerido");

        var id = await _productoRepository.CreateAsync(producto);
        producto.Id = id;

        return producto;
    }

    public async Task<Producto> UpdateProductoAsync(int id, Producto producto)
    {
        if (id != producto.Id)
            throw new ArgumentException("ID del producto no coincide");

        var existingProducto = await _productoRepository.GetByIdAsync(id);
        if (existingProducto == null)
            throw new KeyNotFoundException($"Producto con ID {id} no encontrado");

        var productos = await _productoRepository.GetAllAsync();
        if (productos.Any(p => p.CodigoProducto == producto.CodigoProducto && p.Id != id))
        {
            throw new InvalidOperationException($"Ya existe otro producto con el código {producto.CodigoProducto}");
        }

        var updated = await _productoRepository.UpdateAsync(producto);
        if (!updated)
            throw new Exception("Error al actualizar el producto");

        return producto;
    }

    public async Task<bool> DeleteProductoAsync(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto == null)
            throw new KeyNotFoundException($"Producto con ID {id} no encontrado");

        return await _productoRepository.DeleteAsync(id);
    }

    public async Task<bool> CodigoProductoExistsAsync(int codigoProducto)
    {
        var productos = await _productoRepository.GetAllAsync();
        return productos.Any(p => p.CodigoProducto == codigoProducto);
    }
}