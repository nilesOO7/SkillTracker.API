using Newtonsoft.Json;
using System.Collections.Generic;

namespace SkillTracker.Data.Models
{
    public class Associate
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("strength")]
        public string Strength { get; set; }

        [JsonProperty("weakness")]
        public string Weakness { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("isFresher")]
        public bool IsFresher { get; set; }

        [JsonProperty("otherSkill")]
        public string OtherSkill { get; set; }

        [JsonProperty("pictureBase64String")]
        public string PictureBase64String { get; set; }

        /// <summary>
        /// For Report Purpose
        /// </summary>
        [JsonProperty("strongSkills")]
        public string StrongSkills { get; set; }

        [JsonIgnore]
        public ICollection<AssociateSkill> AssociateSkills { get; set; }
    }
}
