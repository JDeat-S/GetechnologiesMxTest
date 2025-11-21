using System.Net.Http;
using System.Net.Http.Json;
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

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        var response = await _http.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }
}
