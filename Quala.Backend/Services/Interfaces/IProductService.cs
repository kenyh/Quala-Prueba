using Quala.Backend.Models;

namespace Quala.Backend.Services.Interfaces;

public interface IProductoService
{
    Task<IEnumerable<Producto>> GetAllProductosAsync();
    Task<Producto> GetProductoByIdAsync(int id);
    Task<Producto> CreateProductoAsync(Producto producto);
    Task<Producto> UpdateProductoAsync(int id, Producto producto);
    Task<bool> DeleteProductoAsync(int id);
    Task<bool> CodigoProductoExistsAsync(int codigoProducto);
}