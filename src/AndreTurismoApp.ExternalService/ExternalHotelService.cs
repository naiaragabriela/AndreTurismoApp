using System.Net;
using System.Net.Http.Json;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalService
{
    public class ExternalHotelService
    {
        private static readonly HttpClient hotels = new();

        public async Task<List<Hotel>> GetHotel()
        {
            try
            {
                HttpResponseMessage response = await hotels.GetAsync("https://localhost:8080/api/Hotels");
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                List<Hotel>? end = JsonConvert.DeserializeObject<List<Hotel>>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                return new List<Hotel>();
            }
        }
        public async Task<Hotel> GetHotelById(int id)
        {
            try
            {
                HttpResponseMessage response = await hotels.GetAsync("https://localhost:8080/api/Hotels" + id);
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                Hotel? end = JsonConvert.DeserializeObject<Hotel>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
        public async Task<HttpStatusCode> PostHotel(Hotel hotel)
        {
            HttpResponseMessage response = await hotels.PostAsJsonAsync("https://localhost:8080/api/Hotels", hotel);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> PutHotel(Hotel hotel)
        {
            HttpResponseMessage response = await hotels.PutAsJsonAsync("https://localhost:8080/api/Hotels", hotel);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> DeleteHotel(int id)
        {
            HttpResponseMessage response = await hotels.DeleteAsync("https://localhost:8080/api/Hotels" + id);
            return response.StatusCode;
        }
    }
}
