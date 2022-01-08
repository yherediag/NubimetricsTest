using System.Collections.Generic;

namespace API.DTOs
{
    public class PaisesDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string locale { get; set; }
        public string currency_id { get; set; }
    }

    public class ArgentinaDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string locale { get; set; }
        public string currency_id { get; set; }
        public string decimal_separator { get; set; }
        public string thousands_separator { get; set; }
        public string time_zone { get; set; }
        public object geo_information { get; set; }
        public List<State> states { get; set; }

        public class State
        {
            public string id { get; set; }
            public string name { get; set; }
        }
    }
}
