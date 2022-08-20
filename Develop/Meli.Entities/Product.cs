﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Meli.Entities
{
    public class Product
    {
        [Required]
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("site_id")]
        public string SiteId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("base_price")]
        public double BasePrice { get; set; }
        [JsonIgnore]
        public int likesNumber { get; set; }
    }
}