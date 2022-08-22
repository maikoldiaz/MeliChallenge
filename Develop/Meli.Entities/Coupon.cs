using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Meli.Entities
{
    public class Coupon
    {
        [Required]
        [JsonPropertyName("item_ids")]
        public List<string> ItemIds { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}
