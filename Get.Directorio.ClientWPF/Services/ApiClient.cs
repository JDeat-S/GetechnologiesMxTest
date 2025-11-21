using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient()
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44340/") //cambia el puerto al levantar proyecto
        };
    }
    public async Task<TOut> PostAsync<TIn, TOut>(string url, TIn data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var respJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TOut>(respJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
    public async Task<T> GetAsync<T>(string url)
    {
        var response = await _http.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
