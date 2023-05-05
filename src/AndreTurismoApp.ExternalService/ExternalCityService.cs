using System.Net;
using System.Net.Http.Json;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalService
{
    public class ExternalCityService
    {
        private static readonly HttpClient cities = new();


        public async Task<List<City>> GetCity()
        {
            try
            {
                HttpResponseMessage response = await cities.GetAsync("https://localhost:8082/api/Cities");
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                List<City>? end = JsonConvert.DeserializeObject<List<City>>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                return new List<City>();
            }
        }

        public async Task<City> GetCityById(int id)
        {
            try
            {
                HttpResponseMessage response = await cities.GetAsync("https://localhost:8082/api/Cities/" + id);
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                City? end = JsonConvert.DeserializeObject<City>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> PostCity(City city)
        {
            HttpResponseMessage response = await cities.PostAsJsonAsync("https://localhost:8082/api/Cities", city);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutCity(City city)
        {
            HttpResponseMessage response = await cities.PutAsJsonAsync("https://localhost:8082/api/Cities", city);
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> DeleteCity(int id)
        {
            HttpResponseMessage response = await cities.DeleteAsync("https://localhost:8082/api/Cities/" + id);
            return response.StatusCode;
        }

    }
}