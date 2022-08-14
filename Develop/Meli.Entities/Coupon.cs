namespace Meli.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class Coupon
{
    [Required]
    [JsonPropertyName("item_ids")]
    public List<string>? ItemIds { get; set; }
    [JsonPropertyName("amount")]
    public double Amount { get; set; }
}

