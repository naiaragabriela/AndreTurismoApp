using System.Net;
using System.Net.Http.Json;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalService
{
    public class ExternalPackageService
    {
        private static readonly HttpClient packages = new();
        public async Task<List<Package>> GetPackage()
        {
            try
            {
                HttpResponseMessage response = await packages.GetAsync("https://localhost:8084/api/Packages");
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                List<Package>? end = JsonConvert.DeserializeObject<List<Package>>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                return new List<Package>();
            }
        }
        public async Task<Package> GetPackageById(int id)
        {
            try
            {
                HttpResponseMessage response = await packages.GetAsync("https://localhost:8084/api/Packages/" + id);
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                Package? end = JsonConvert.DeserializeObject<Package>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
        public async Task<HttpStatusCode> PostPackage(Package package)
        {
            HttpResponseMessage response = await packages.PostAsJsonAsync("https://localhost:8084/api/Packages", packages);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> PutPackage(Package package)
        {
            HttpResponseMessage response = await packages.PutAsJsonAsync("https://localhost:8084/api/Packages", packages);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> DeletePackage(int id)
        {
            HttpResponseMessage response = await packages.DeleteAsync("https://localhost:8084/api/Packages/" + id);
            return response.StatusCode;
        }
    }
}
