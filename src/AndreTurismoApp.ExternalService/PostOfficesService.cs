using AndreTurismoApp.Models.DTO;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalService
{
    public class PostOfficesService
    {
        private static readonly HttpClient endereco = new();
        public static async Task<AddressDTO> GetAddress(string cep)
        {
            try
            {
                HttpResponseMessage response = await endereco.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                _ = response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                AddressDTO? end = JsonConvert.DeserializeObject<AddressDTO>(ender);
                return end;
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }
    }
}
