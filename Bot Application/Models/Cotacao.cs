using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot_Application.Models
{
    public class Cotacao
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("sigla")]
        public string Sigla { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }
    }
}