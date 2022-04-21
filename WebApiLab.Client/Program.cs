using System.Text.Json;

Console.Write("ProductId: ");
var id = Console.ReadLine();
await GetProductAsync(int.Parse(id));

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