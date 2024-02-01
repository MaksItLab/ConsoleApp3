using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ConsoleApp3
{

    public class Request
    {
        public string Query { get; set; }
    }

    public class Response
    {
        public List<Country> Suggestions { get; set; }
    }

    public class Country
    {
        public string Value { get; set; }
        public string Unrestricted_value { get; set; }

        public Data Data { get; set; }
    }

    public class Data
    {
        public string Code { get; set; }
        public string Name_short { get; set; }
        public string Name { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            string countryName = Console.ReadLine();
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Token 9016aa09370b2a2c57ca7b6dc4a88d8c5bdfc066");

            var response = await httpClient.PostAsJsonAsync("http://suggestions.dadata.ru/suggestions/api/4_1/rs/suggest/country", new Request { Query = countryName});

            var message = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<Response>();

            foreach (var item in message.Suggestions)
                {
                    Console.WriteLine(item.Value + " : " + item.Unrestricted_value + " : " + item.Data.Code);
                }
        }
    }
}
