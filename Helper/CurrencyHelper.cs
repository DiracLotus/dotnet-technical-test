using dotnet_technical_test.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnet_technical_test.Helper
{
    public class CurrencyHelper
    {
        /// <summary>
        /// Pulls down a list of all currency conversions from the given API.
        /// </summary>
        /// <param name="apiUrl">Base API url.</param>
        /// <param name="code">Three letter code of the currency to fetch rates for.</param>
        public async Task<Currency> GetCurrencyAsync(string apiUrl, string code)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                HttpResponseMessage response = await client.GetAsync($"{apiUrl}/{code}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Currency>(data);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
