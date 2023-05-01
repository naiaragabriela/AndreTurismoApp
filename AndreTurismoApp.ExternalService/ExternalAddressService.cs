using System.Net;
using System.Net.Http.Json;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalService
{
    public class ExternalAddressService
    {

        static readonly HttpClient addresses = new HttpClient();
        public async Task<List<Address>> GetAddress()
        {
            try
            {
                HttpResponseMessage response = await addresses.GetAsync("https://localhost:8081/api/Addresses");
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<Address>>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return new List<Address>();
            }
        }

        public async Task<Address> GetAddressById(int id)
        {
            try
            {
                HttpResponseMessage response = await addresses.GetAsync("https://localhost:8081/api/Addresses/" + id);
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<Address>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> PostAddress(Address address)
        {
            HttpResponseMessage response = await addresses.PostAsJsonAsync("https://localhost:8081/api/Addresses", address);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutAddress(Address address)
        {
            HttpResponseMessage response = await addresses.PutAsJsonAsync("https://localhost:8081/api/Addresses", address);
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> DeleteAddress(int id)
        {
            HttpResponseMessage response = await addresses.DeleteAsync("https://localhost:8081/api/Addresses/" + id);
            return response.StatusCode;
        }
    }
}
