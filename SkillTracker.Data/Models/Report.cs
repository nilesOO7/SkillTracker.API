using Newtonsoft.Json;
using System.Collections.Generic;

namespace SkillTracker.Data.Models
{
    public class Report
    {
        [JsonProperty("skillSpread")]
        public List<AssociateSkill> SkillSpread { get; set; }

        [JsonProperty("registeredCandidate")]
        public int RegisteredCandidateCount { get; set; }

        [JsonProperty("femaleCandidatePercentage")]
        public decimal FemaleCandidatePercentage { get; set; }

        [JsonProperty("maleCandidatePercentage")]
        public decimal MaleCandidatePercentage { get; set; }

        [JsonProperty("fresherCandidatePercentage")]
        public decimal FresherCandidatePercentage { get; set; }

        [JsonProperty("ratedCandidate")]
        public int RatedCandidateCount { get; set; }

        [JsonProperty("femaleRatedCandidatePercentage")]
        public decimal FemaleRatedCandidatePercentage { get; set; }

        [JsonProperty("maleRatedCandidatePercentage")]
        public decimal MaleRatedCandidatePercentage { get; set; }

        [JsonProperty("l1CandidatePercentage")]
        public decimal Level1CandidatePercentage { get; set; }

        [JsonProperty("l2CandidatePercentage")]
        public decimal Level2CandidatePercentage { get; set; }

        [JsonProperty("l3CandidatePercentage")]
        public decimal Level3CandidatePercentage { get; set; }
    }
}
