using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using TaxiFarePrediction.Mobile.Models;

namespace TaxiFarePrediction.Mobile.Services
{
    public static class PredictionService
    {
        const string PredictionEndpoint = "https://taxifarepredictionweb7.azurewebsites.net/";
        const string TaxiFarePredictionService = "api/Predict";

        private static readonly HttpClient client = CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(PredictionEndpoint);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        public async static Task<float> Predict(TaxiTrip trip)
        {
            try
            {
                var json = JsonConvert.SerializeObject(trip);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(TaxiFarePredictionService, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return float.Parse(result);
                }
            }
            catch (Exception ex)
            {
            }

            return 0;
        }
    }
}
