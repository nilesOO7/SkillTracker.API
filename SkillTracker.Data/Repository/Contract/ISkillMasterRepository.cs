using System.Collections.Generic;
using SkillTracker.Data.Models;

namespace SkillTracker.Data.Repository.Contract
{
    public interface ISkillMasterRepository
    {
        List<Skill> GetSkills();
        Skill GetSkill(int skillId, bool preserveState);
        int AddSkill(Skill inputSkill);
        int EditSkill(Skill inputSkill);
        int DeleteSkill(Skill inputSkill);
    }
}
