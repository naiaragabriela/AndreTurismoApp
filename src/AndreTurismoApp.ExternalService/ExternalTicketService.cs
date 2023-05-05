using System.Net;
using System.Net.Http.Json;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalService
{
    public class ExternalTicketService
    {
        private static readonly HttpClient tickets = new();
        public async Task<List<Ticket>> GetTicket()
        {
            try
            {
                HttpResponseMessage response = await tickets.GetAsync("https://localhost:8085/api/Tickets");
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                List<Ticket>? end = JsonConvert.DeserializeObject<List<Ticket>>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                return new List<Ticket>();
            }
        }
        public async Task<Ticket> GetTicketById(int id)
        {
            try
            {
                HttpResponseMessage response = await tickets.GetAsync("https://localhost:8085/api/Tickets/" + id);
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                Ticket? end = JsonConvert.DeserializeObject<Ticket>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
        public async Task<HttpStatusCode> PostTicket(Ticket ticket)
        {
            HttpResponseMessage response = await tickets.PostAsJsonAsync("https://localhost:8085/api/Tickets", ticket);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> PutTicket(Ticket ticket)
        {
            HttpResponseMessage response = await tickets.PutAsJsonAsync("https://localhost:8085/api/Tickets", ticket);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> DeleteTicket(int id)
        {
            HttpResponseMessage response = await tickets.DeleteAsync("https://localhost:8085/api/Tickets/" + id);
            return response.StatusCode;
        }
    }
}
