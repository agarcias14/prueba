using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Api.Dtos;
using PruebaTecnica.Api.Services;

namespace PruebaTecnica.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ProductosService _service;

    public ProductosController(ProductosService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? codigo, [FromQuery] string? nombre, [FromQuery] bool? activo)
        => Ok(await _service.GetAllAsync(codigo, nombre, activo));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound(new { message = "Producto no encontrado." }) : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductoCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.CreateAsync(dto);
        if (!result.ok) return BadRequest(new { message = result.error });

        return CreatedAtAction(nameof(GetById), new { id = result.created!.Id }, result.created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductoUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.UpdateAsync(id, dto);
        if (!result.ok)
            return result.error == "No encontrado."
                ? NotFound(new { message = result.error })
                : BadRequest(new { message = result.error });

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var result = await _service.SoftDeleteAsync(id);
        return result.ok ? NoContent() : NotFound(new { message = result.error });
    }
}