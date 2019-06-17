using Newtonsoft.Json;

namespace SkillTracker.Data.Models
{
    [ExcludeFromCoverage]
    public class Image
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("base64StringSrc")]
        public string Base64StringSrc { get; set; }
    }
}
