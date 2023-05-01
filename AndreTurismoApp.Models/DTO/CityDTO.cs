﻿using Newtonsoft.Json;

namespace AndreTurismoApp.Models.DTO
{
    public class CityDTO
    {
        public int IdCity { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }
    }
}
