using System.Collections.Generic;
using SkillTracker.Data.Models;
using SkillTracker.Data.Provider.Contract;
using SkillTracker.Data.Repository.Contract;
using SkillTracker.Data.Repository.Implementation;

namespace SkillTracker.Data.Provider.Implementation
{
    public class SkillMasterProvider : ISkillMasterProvider
    {
        private ISkillMasterRepository _skillMasterRepo;

        [ExcludeFromCoverage]
        public SkillMasterProvider(ISkillMasterRepository skillRepo)
        {
            if (skillRepo != null)
            {
                this._skillMasterRepo = skillRepo;
            }
            else
            {
                this._skillMasterRepo = new SkillMasterRepository();
            }
        }

        public List<Skill> GetSkills()
        {
            return this._skillMasterRepo.GetSkills();
        }

        public Skill GetSkill(int skillId, bool preserveState)
        {
            return this._skillMasterRepo.GetSkill(skillId, preserveState);
        }

        public int AddSkill(Skill inputSkill)
        {
            return this._skillMasterRepo.AddSkill(inputSkill);
        }

        public int EditSkill(Skill inputSkill)
        {
            return this._skillMasterRepo.EditSkill(inputSkill);
        }

        public int DeleteSkill(Skill inputSkill)
        {
            return this._skillMasterRepo.DeleteSkill(inputSkill);
        }
    }
}
