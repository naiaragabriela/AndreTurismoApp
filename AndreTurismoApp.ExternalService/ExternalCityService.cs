using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AndreTurismoApp.Models;
using AndreTurismoApp.Models.DTO;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalService
{
    public class ExternalCityService
    {
        static readonly HttpClient cities = new HttpClient();
  

        public async Task<List<City>> GetCity()
        {
            try
            {
                HttpResponseMessage response = await cities.GetAsync("https://localhost:8082/api/Cities");
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<City>>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return new List<City>();
            }
        }

        public async Task<City> GetCityById(int id)
        {
            try
            {
                HttpResponseMessage response = await cities.GetAsync("https://localhost:8082/api/Cities/"+ id);
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<City>(ender);
                return end;
            }
            catch (HttpRequestException e)
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
            HttpResponseMessage response = await cities.DeleteAsync("https://localhost:8082/api/Cities/"+ id);
            return response.StatusCode;
        }

    }
}