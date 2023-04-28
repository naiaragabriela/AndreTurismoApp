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
        [JsonProperty("localidade")]
        public string City { get; set; }
    }
}
