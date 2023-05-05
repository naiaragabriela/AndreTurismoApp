using System.Net;
using System.Net.Http.Json;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalService
{
    public class ExternalCustomerService
    {
        private static readonly HttpClient clients = new();
        public async Task<List<Customer>> GetCustomers()
        {
            try
            {
                HttpResponseMessage response = await clients.GetAsync("https://localhost:8083/api/Customers");
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                List<Customer>? end = JsonConvert.DeserializeObject<List<Customer>>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                return new List<Customer>();
            }
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            try
            {
                HttpResponseMessage response = await clients.GetAsync("https://localhost:8083/api/Customers/" + id);
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                Customer? end = JsonConvert.DeserializeObject<Customer>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> PostCustomer(Customer client)
        {
            HttpResponseMessage response = await clients.PostAsJsonAsync("https://localhost:8083/api/Customers", client);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutCustomer(Customer client)
        {
            HttpResponseMessage response = await clients.PutAsJsonAsync("https://localhost:8083/api/Customers", client);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteCustomer(int id)
        {
            HttpResponseMessage response = await clients.DeleteAsync("https://localhost:8083/api/Customers/" + id);
            return response.StatusCode;
        }

    }
}
