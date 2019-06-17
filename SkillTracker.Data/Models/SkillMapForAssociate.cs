using Newtonsoft.Json;

namespace SkillTracker.Data.Models
{
    public class SkillMapForAssociate
    {
        [JsonProperty("skillId")]
        public int SkillId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isTechnical")]
        public bool IsTechnical { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("isSelected")]
        public bool IsSelected { get; set; }
    }
}
