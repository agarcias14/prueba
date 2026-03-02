using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace PruebaTecnica.Ui.Services;

public class ProductosApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductosApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("Api");

    public async Task<List<ProductoDto>> GetAllAsync(string? codigo = null, string? nombre = null, bool? activo = null)
    {
        var query = new List<string>();
        if (!string.IsNullOrWhiteSpace(codigo)) query.Add($"codigo={Uri.EscapeDataString(codigo)}");
        if (!string.IsNullOrWhiteSpace(nombre)) query.Add($"nombre={Uri.EscapeDataString(nombre)}");
        if (activo.HasValue) query.Add($"activo={activo.Value.ToString().ToLower()}");

        var url = "api/Productos";
        if (query.Count > 0) url += "?" + string.Join("&", query);

        var data = await Client.GetFromJsonAsync<List<ProductoDto>>(url);
        return data ?? new List<ProductoDto>();
    }

    public async Task<ProductoDto?> GetByIdAsync(int id)
        => await Client.GetFromJsonAsync<ProductoDto>($"api/Productos/{id}");

    public async Task<ProductoDto?> CreateAsync(ProductoCreateDto dto)
    {
        var resp = await Client.PostAsJsonAsync("api/Productos", dto);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<ProductoDto>();
    }

    public async Task<bool> UpdateAsync(int id, ProductoUpdateDto dto)
    {
        var resp = await Client.PutAsJsonAsync($"api/Productos/{id}", dto);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var resp = await Client.DeleteAsync($"api/Productos/{id}");
        return resp.IsSuccessStatusCode;
    }
}

public class ProductoDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = "";
    public string Nombre { get; set; } = "";
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public bool Activo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ProductoCreateDto
{
    [Required(ErrorMessage = "El código es obligatorio")]
    [StringLength(20, ErrorMessage = "El código no puede tener más de 20 caracteres")]
    public string Codigo { get; set; } = "";

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }
}

public class ProductoUpdateDto
{
    [Required(ErrorMessage = "El código es obligatorio")]
    [StringLength(20, ErrorMessage = "El código no puede tener más de 20 caracteres")]
    public string Codigo { get; set; } = "";

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }

    public bool Activo { get; set; }
}