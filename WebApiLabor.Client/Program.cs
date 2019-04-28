using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiLabor.Client.Api;

namespace WebApiLabor.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("ProductId: ");
            var id = Console.ReadLine();
            //await GetProductAsync(Int32.Parse(id));
            var p = await GetProduct2Async(int.Parse(id));
            Console.WriteLine(p.Name);
            Console.ReadKey();
        }

        public static async Task GetProductAsync(int id)
        {
            using (var client = new HttpClient())
            {
                /*Port-ot írjuk át a szervernek megfelelően*/
                var response = await client.
                    GetAsync(new Uri($"http://localhost:5000/api/Products/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(json);
                }
            }
        }

        public static async Task<Product> GetProduct2Async(int id)
        {
            using (var httpClient = new HttpClient())
            {
                ProductsClient client= new ProductsClient(httpClient);
                return await client.GetAsync(id);                
            }
        }

    }
}
