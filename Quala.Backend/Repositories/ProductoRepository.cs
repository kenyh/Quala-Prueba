using Dapper;
using Quala.Backend.Data;
using Quala.Backend.Models;
using Quala.Backend.Repositories.Interfaces;
using System.Data;
using static Quala.Backend.Constants.AppConstants;



namespace Quala.Backend.Repositories;


public class ProductoRepository : IProductoRepository
{
    private readonly QualaDbContext _context;

    public ProductoRepository(QualaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Producto>(
            StoredProcedures.ProductGetAll,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Producto> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await connection.QueryFirstOrDefaultAsync<Producto>(
            StoredProcedures.ProductGetById,
            parameters,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int> CreateAsync(Producto producto)
    {
        using var connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@CodigoProducto", producto.CodigoProducto);
        parameters.Add("@Nombre", producto.Nombre);
        parameters.Add("@Descripcion", producto.Descripcion);
        parameters.Add("@ReferenciaInterna", producto.ReferenciaInterna);
        parameters.Add("@PrecioUnitario", producto.PrecioUnitario);
        parameters.Add("@Estado", producto.Estado);
        parameters.Add("@UnidadMedida", producto.UnidadMedida);

        var result = await connection.QueryFirstOrDefaultAsync<int>(
            StoredProcedures.ProductCreate,
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }

    public async Task<bool> UpdateAsync(Producto producto)
    {
        using var connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Id", producto.Id);
        parameters.Add("@CodigoProducto", producto.CodigoProducto);
        parameters.Add("@Nombre", producto.Nombre);
        parameters.Add("@Descripcion", producto.Descripcion);
        parameters.Add("@ReferenciaInterna", producto.ReferenciaInterna);
        parameters.Add("@PrecioUnitario", producto.PrecioUnitario);
        parameters.Add("@Estado", producto.Estado);
        parameters.Add("@UnidadMedida", producto.UnidadMedida);

        var rowsAffected = await connection.ExecuteScalarAsync<int>(
            StoredProcedures.ProductUpdate,
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        var rowsAffected = await connection.ExecuteScalarAsync<int>(
            StoredProcedures.ProductDelete,
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return rowsAffected > 0;
    }

    public async Task<Producto> GetByCodigoAsync(int codigoProducto)
    {
        using var connection = _context.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@CodigoProducto", codigoProducto);

        return await connection.QueryFirstOrDefaultAsync<Producto>(
            StoredProcedures.ProductGetByCodigo, 
            parameters,
            commandType: CommandType.StoredProcedure
        );
    }
}
