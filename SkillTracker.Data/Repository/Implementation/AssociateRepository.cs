using System.Collections.Generic;
using System.Linq;
using SkillTracker.Data.EFCore;
using SkillTracker.Data.Models;
using SkillTracker.Data.Repository.Contract;

namespace SkillTracker.Data.Repository.Implementation
{
    public class AssociateRepository : IAssociateRepository
    {
        private SkillTrackerDbContext _skillTrackerDbContext;

        public AssociateRepository()
        {
            this._skillTrackerDbContext = new SkillTrackerDbContext();
        }

        public List<Associate> GetAssociates()
        {
            return this._skillTrackerDbContext.Associates.ToList();
        }

        public Associate GetAssociate(int skillId, bool preserveState)
        {
            if (preserveState)
                return this._skillTrackerDbContext.Associates.Where(w => w.Id == skillId).FirstOrDefault<Associate>();
            else
                return this._skillTrackerDbContext.Associates.AsNoTracking().Where(w => w.Id == skillId).FirstOrDefault<Associate>();
        }

        public int AddAssociate(Associate inputAssociate)
        {
            int rec = 0;
            this._skillTrackerDbContext.Associates.Add(inputAssociate);
            rec = this._skillTrackerDbContext.SaveChanges();
            return rec;
        }

        public int EditAssociate(Associate inputAssociate)
        {
            int rec = 0;
            this._skillTrackerDbContext.Entry(inputAssociate).State = System.Data.Entity.EntityState.Modified;
            rec = this._skillTrackerDbContext.SaveChanges();
            return rec;
        }

        public int DeleteAssociate(Associate inputAssociate)
        {
            int rec = 0;

            var assocWithSkill = this._skillTrackerDbContext.AssociateSkills.AsNoTracking().Where(w => w.AssociateId == inputAssociate.Id).Select(s => s.SkillId).ToList();

            if (assocWithSkill != null && assocWithSkill.Count > 0)
            {
                this._skillTrackerDbContext.AssociateSkills.Where(w => assocWithSkill.Contains(w.SkillId) && w.AssociateId == inputAssociate.Id)
                    .ToList()
                    .ForEach(f =>
                    {
                        this._skillTrackerDbContext.AssociateSkills.Remove(f);
                        rec = this._skillTrackerDbContext.SaveChanges();
                    });
            }

            this._skillTrackerDbContext.Associates.Remove(inputAssociate);
            rec = this._skillTrackerDbContext.SaveChanges();
            return rec;
        }
    }
}
