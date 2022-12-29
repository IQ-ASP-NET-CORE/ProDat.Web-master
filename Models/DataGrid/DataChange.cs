using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models.DataGrid
{
    public class DataChange
    {
            [JsonProperty("key")]
            public object Key { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
     
    }
}
