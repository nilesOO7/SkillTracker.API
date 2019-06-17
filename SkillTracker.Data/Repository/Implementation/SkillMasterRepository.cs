using System.Collections.Generic;
using System.Linq;
using SkillTracker.Data.EFCore;
using SkillTracker.Data.Models;
using SkillTracker.Data.Repository.Contract;

namespace SkillTracker.Data.Repository.Implementation
{
    public class SkillMasterRepository : ISkillMasterRepository
    {
        private SkillTrackerDbContext _skillTrackerDbContext;

        public SkillMasterRepository()
        {
            this._skillTrackerDbContext = new SkillTrackerDbContext();
        }

        public List<Skill> GetSkills()
        {
            return this._skillTrackerDbContext.Skills.ToList();
        }

        public Skill GetSkill(int skillId, bool preserveState)
        {
            if (preserveState)
                return this._skillTrackerDbContext.Skills.Where(w => w.Id == skillId).FirstOrDefault<Skill>();
            else
                return this._skillTrackerDbContext.Skills.AsNoTracking().Where(w => w.Id == skillId).FirstOrDefault<Skill>();
        }

        public int AddSkill(Skill inputSkill)
        {
            int rec = 0;
            this._skillTrackerDbContext.Skills.Add(inputSkill);
            rec = this._skillTrackerDbContext.SaveChanges();
            rec = rec > 0 ? inputSkill.Id : rec;
            return rec;
        }

        public int EditSkill(Skill inputSkill)
        {
            int rec = 0;
            this._skillTrackerDbContext.Entry(inputSkill).State = System.Data.Entity.EntityState.Modified;
            rec = this._skillTrackerDbContext.SaveChanges();
            return rec;
        }

        public int DeleteSkill(Skill inputSkill)
        {
            int rec = 0;

            var skillWithAssoc = this._skillTrackerDbContext.AssociateSkills.AsNoTracking().Where(w => w.SkillId == inputSkill.Id).Select(s => s.AssociateId).ToList();

            if (skillWithAssoc.Count > 0)
            {
                this._skillTrackerDbContext.AssociateSkills.Where(w => skillWithAssoc.Contains(w.AssociateId) && w.SkillId == inputSkill.Id)
                    .ToList()
                    .ForEach(f =>
                    {
                        this._skillTrackerDbContext.AssociateSkills.Remove(f);
                        rec = this._skillTrackerDbContext.SaveChanges();
                    });
            }

            this._skillTrackerDbContext.Skills.Remove(inputSkill);
            rec = this._skillTrackerDbContext.SaveChanges();
            return rec;
        }
    }
}
