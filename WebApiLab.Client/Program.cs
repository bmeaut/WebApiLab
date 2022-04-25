using System.Text.Json;

using WebApiLab.Client.Api;

Console.Write("ProductId: ");
var id = Console.ReadLine();
var p = await GetProduct2Async(int.Parse(id));
Console.WriteLine($"{p.Name}: {p.UnitPrice}");
Console.ReadKey();

static async Task GetProductAsync(int id)
{
    using var client = new HttpClient();

    /*A portot írjuk át a szervernek megfelelően*/
    var response = await client.GetAsync(new Uri($"http://localhost:5184/api/Products/{id}"));
    response.EnsureSuccessStatusCode();
    var jsonStream = await response.Content.ReadAsStreamAsync();
    var json = await JsonDocument.ParseAsync(jsonStream);
    Console.WriteLine($"{json.RootElement.GetProperty("name")}:" +
        $"{json.RootElement.GetProperty("unitPrice")}.-");
}

static async Task<Product> GetProduct2Async(int id)
{
    using var httpClient = new HttpClient();
    var client = new ProductClient("http://localhost:5184", httpClient);
    return await client.GetAsync(id);
}