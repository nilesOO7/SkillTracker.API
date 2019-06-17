using System.Collections.Generic;
using System.Linq;
using SkillTracker.Data.EFCore;
using SkillTracker.Data.Models;
using SkillTracker.Data.Repository.Contract;

namespace SkillTracker.Data.Repository.Implementation
{
    public class AssociateSkillMapRepository : IAssociateSkillMapRepository
    {
        private SkillTrackerDbContext _skillTrackerDbContext;

        public AssociateSkillMapRepository()
        {
            this._skillTrackerDbContext = new SkillTrackerDbContext();
        }

        public List<AssociateSkill> GetAssociateSkillMappings()
        {
            return this._skillTrackerDbContext.AssociateSkills.ToList();
        }

        public int AddAssociateSkillMapping(List<AssociateSkill> inputSkillMap)
        {
            int rec = 0;
            this._skillTrackerDbContext.AssociateSkills.AddRange(inputSkillMap);
            rec = this._skillTrackerDbContext.SaveChanges();
            return rec;
        }

        public int DeleteAssociateSkillMapping(int associateId)
        {
            int rec = 0;

            var assocWithSkill = this._skillTrackerDbContext.AssociateSkills.AsNoTracking().Where(w => w.AssociateId == associateId).Select(s => s.SkillId).ToList();

            if (assocWithSkill != null && assocWithSkill.Count > 0)
            {
                this._skillTrackerDbContext.AssociateSkills.Where(w => assocWithSkill.Contains(w.SkillId) && w.AssociateId == associateId)
                    .ToList()
                    .ForEach(f =>
                    {
                        this._skillTrackerDbContext.AssociateSkills.Remove(f);
                        rec += this._skillTrackerDbContext.SaveChanges();
                    });
            }

            return rec;
        }
    }
}
