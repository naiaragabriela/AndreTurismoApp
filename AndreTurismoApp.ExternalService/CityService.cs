using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using AndreTurismoApp.Models;
using AndreTurismoApp.Models.DTO;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalService
{
    public class CityService
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


        public async Task<List<City>> GetCityById(int id)
        {
            try
            {
                HttpResponseMessage response = await cities.GetAsync("https://localhost:8082/api/Cities"+ id);
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<City>>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }







    }
}