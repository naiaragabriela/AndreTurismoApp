using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalService
{
    public class ExternalClientService
    {

        static readonly HttpClient clients = new HttpClient();
        public async Task<List<Client>> GetAddress()
        {
            try
            {
                HttpResponseMessage response = await clients.GetAsync("https://localhost:8083/api/Clients");
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<List<Client>>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return new List<Client>();
            }
        }

        public async Task<Client> GetAddressById(int id)
        {
            try
            {
                HttpResponseMessage response = await clients.GetAsync("https://localhost:8083/api/Clients/" + id);
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<Client>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> PostClient(Client client)
        {
            HttpResponseMessage response = await clients.PostAsJsonAsync("https://localhost:8083/api/Clients", client);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutClient(Client client)
        {
            HttpResponseMessage response = await clients.PutAsJsonAsync("https://localhost:8083/api/Clients", client);
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> DeleteClient(int id)
        {
            HttpResponseMessage response = await clients.DeleteAsync("https://localhost:8083/api/Clients/" + id);
            return response.StatusCode;
        }

    }
}
