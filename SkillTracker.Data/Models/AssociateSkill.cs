using Newtonsoft.Json;

namespace SkillTracker.Data.Models
{
    public class AssociateSkill
    {
        [JsonProperty("associateId")]
        public int AssociateId { get; set; }

        [JsonProperty("skillId")]
        public int SkillId { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        /// <summary>
        /// For Report Purpose
        /// </summary>
        [JsonProperty("skillName")]
        public string SkillName { get; set; }

        /// <summary>
        /// For Report Purpose
        /// </summary>
        [JsonProperty("percentage")]
        public decimal Percentage { get; set; }

        [JsonIgnore]
        public Associate Associate { get; set; }

        [JsonIgnore]
        public Skill Skill { get; set; }
    }
}
