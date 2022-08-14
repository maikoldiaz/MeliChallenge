using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Meli.Entities;
public class CouponResponse
{
    [JsonPropertyName("item_ids")]
    public List<string>? ItemIds { get; set; }
    [JsonPropertyName("total")]
    public double Total { get; set; }
}

