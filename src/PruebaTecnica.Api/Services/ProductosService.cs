using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Api.Data;
using PruebaTecnica.Api.Dtos;
using PruebaTecnica.Api.Models;

namespace PruebaTecnica.Api.Services;

public class ProductosService
{
    private readonly AppDbContext _db;

    public ProductosService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Producto>> GetAllAsync(string? codigo, string? nombre, bool? activo)
    {
        var q = _db.Productos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(codigo))
            q = q.Where(p => p.Codigo.Contains(codigo));

        if (!string.IsNullOrWhiteSpace(nombre))
            q = q.Where(p => p.Nombre.Contains(nombre));

        if (activo.HasValue)
            q = q.Where(p => p.Activo == activo.Value);

        return await q.OrderBy(p => p.Id).ToListAsync();
    }

    public async Task<Producto?> GetByIdAsync(int id)
        => await _db.Productos.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<(bool ok, string? error, Producto? created)> CreateAsync(ProductoCreateDto dto)
    {
        var codigo = dto.Codigo.Trim();
        var nombre = dto.Nombre.Trim();

        var exists = await _db.Productos.AnyAsync(p => p.Codigo == codigo);
        if (exists) return (false, "El Código ya existe.", null);

        var entity = new Producto
        {
            Codigo = codigo,
            Nombre = nombre,
            Precio = dto.Precio,
            Stock = dto.Stock,
            Activo = true,
            CreatedAt = DateTime.UtcNow
        };

        _db.Productos.Add(entity);
        await _db.SaveChangesAsync();

        return (true, null, entity);
    }

    public async Task<(bool ok, string? error)> UpdateAsync(int id, ProductoUpdateDto dto)
    {
        var entity = await _db.Productos.FirstOrDefaultAsync(p => p.Id == id);
        if (entity is null) return (false, "No encontrado.");

        var codigo = dto.Codigo.Trim();
        var nombre = dto.Nombre.Trim();

        var codigoTaken = await _db.Productos.AnyAsync(p => p.Codigo == codigo && p.Id != id);
        if (codigoTaken) return (false, "El Código ya existe en otro producto.");

        entity.Codigo = codigo;
        entity.Nombre = nombre;
        entity.Precio = dto.Precio;
        entity.Stock = dto.Stock;
        entity.Activo = dto.Activo;
        entity.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool ok, string? error)> SoftDeleteAsync(int id)
    {
        var entity = await _db.Productos.FirstOrDefaultAsync(p => p.Id == id);
        if (entity is null) return (false, "No encontrado.");

        entity.Activo = false;
        entity.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return (true, null);
    }
}