using Quala.Backend.Models;

namespace Quala.Backend.Repositories.Interfaces;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto> GetByIdAsync(int id);
    Task<int> CreateAsync(Producto producto);
    Task<bool> UpdateAsync(Producto producto);
    Task<bool> DeleteAsync(int id);
    Task<Producto> GetByCodigoAsync(int codigoProducto);
}