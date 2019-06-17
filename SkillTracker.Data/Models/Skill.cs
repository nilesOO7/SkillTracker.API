using System.Collections.Generic;
using Newtonsoft.Json;

namespace SkillTracker.Data.Models
{
    public class Skill
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isTechnical")]
        public bool IsTechnical { get; set; }

        [JsonIgnore]
        public ICollection<AssociateSkill> AssociateSkills { get; set; }
    }
}
