using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AndreTurismoApp.Models.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }

        [JsonProperty("pais")]
        public string? Country { get; set; }
        [JsonProperty("cep")]
        public string CEP { get; set; }
        [JsonProperty("bairro")]
        public string Neighborhood { get; set; }
        [JsonProperty("localidade")] 
        public string City { get; set;}
        [JsonProperty("uf")]
        public string State { get; set; }
        [JsonProperty("logradouro")]
        public string Street { get; set; }
        [JsonProperty ("complemento")]
        public string Complement { get; set; }

    }
}
