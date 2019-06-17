using System.Collections.Generic;
using SkillTracker.Data.Models;

namespace SkillTracker.Data.Repository.Contract
{
    public interface IAssociateSkillMapRepository
    {
        List<AssociateSkill> GetAssociateSkillMappings();
        int AddAssociateSkillMapping(List<AssociateSkill> inputSkillMap);
        int DeleteAssociateSkillMapping(int associateId);
    }
}
