using System.Collections.Generic;
using SkillTracker.Data.Models;

namespace SkillTracker.Data.Provider.Contract
{
    public interface ISkillMasterProvider
    {
        List<Skill> GetSkills();
        Skill GetSkill(int skillId, bool preserveState);
        int AddSkill(Skill inputSkill);
        int EditSkill(Skill inputSkill);
        int DeleteSkill(Skill inputSkill);
    }
}
