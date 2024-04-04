using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CDN.Common
{
    public class FreeLancer
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("Username")]
        public string? Username { get; set; }

        [JsonPropertyName("Mail")]
        public string? Mail { get; set; }

        [JsonPropertyName("HP")]
        public string? HP { get; set; }

        [JsonPropertyName("Skills")]
        public string? Skills { get; set; }

        [JsonPropertyName("Hobby")]
        public string? Hobby { get; set; }
    }
}
