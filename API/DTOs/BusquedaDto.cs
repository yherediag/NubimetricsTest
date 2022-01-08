using System.Collections.Generic;

namespace API.DTOs
{
    public class BusquedaDto
    {
        public string site_id { get; set; }
        public string country_default_time_zone { get; set; }
        public string query { get; set; }
        public object paging { get; set; }
        public List<Result> results { get; set; }
        public object sort { get; set; }
        public List<object> available_sorts { get; set; }
        public List<object> filters { get; set; }
        public List<object> available_filters { get; set; }

        public class Result
        {
            public string id { get; set; }
            public string site_id { get; set; }
            public string title { get; set; }
            public Seller seller { get; set; }
            public double price { get; set; }
            public string permalink { get; set; }
        }

        public class Seller
        {
            public int id { get; set; }
        }
    }  
}
