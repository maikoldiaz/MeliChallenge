using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Meli.Entities
{
    public class Product
    {
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("site_id")]
        public string SiteId { get; set; }


        [JsonProperty("title")]
        public string Title { get; set; }


        [JsonProperty("price")]
        public decimal Price { get; set; }


        [JsonProperty("base_price")]
        public decimal BasePrice { get; set; }

        [JsonIgnore]
        public int likesNumber { get; set; }
    }
}