using System.Text.Json.Serialization;

namespace Wemogy.ReleaseVersionAction.Models
{
    public class Tag
    {
        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }
    }
}
