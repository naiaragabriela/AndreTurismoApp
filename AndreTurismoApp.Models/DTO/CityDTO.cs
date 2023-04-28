using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AndreTurismoApp.Models.DTO
{
    public class CityDTO
    {
        public int IdCity { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }
    }
}
