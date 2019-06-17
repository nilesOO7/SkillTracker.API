using SkillTracker.Data.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace SkillTracker.Data.EFCore
{
    [ExcludeFromCoverage]
    ////public class SkillTrackerDbInitializer : DropCreateDatabaseAlways<SkillTrackerDbContext>
    public class SkillTrackerDbInitializer : DropCreateDatabaseIfModelChanges<SkillTrackerDbContext>
    {
        protected override void Seed(SkillTrackerDbContext context)
        {
            /*
            tbl_Skills
            */
            IList<Skill> defaultSkills = new List<Skill>();

            defaultSkills.Add(new Skill { Id = 1, Name = "HTML5", IsTechnical = true });
            defaultSkills.Add(new Skill { Id = 2, Name = "CSS3", IsTechnical = true });
            defaultSkills.Add(new Skill { Id = 3, Name = "Bootstrap", IsTechnical = true });
            defaultSkills.Add(new Skill { Id = 4, Name = "Javascript", IsTechnical = true });
            defaultSkills.Add(new Skill { Id = 5, Name = "Aptitude", IsTechnical = false });
            defaultSkills.Add(new Skill { Id = 6, Name = "Angular 1", IsTechnical = true });
            defaultSkills.Add(new Skill { Id = 7, Name = "Angular 2", IsTechnical = true });
            defaultSkills.Add(new Skill { Id = 8, Name = "React", IsTechnical = true });

            context.Skills.AddRange(defaultSkills);

            /*
            tbl_Associates
            */
            IList<Associate> defaultAssociates = new List<Associate>();

            defaultAssociates.Add(new Associate { Id = 123456, Name = "Sam Jones", Email = "sam@ymail.com", Mobile = "+1 89787878", Picture = "", Status = "Green", Level = 1, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "M", IsFresher = false, OtherSkill = "CSR Related Works" });
            defaultAssociates.Add(new Associate { Id = 112334, Name = "Clara Matthew", Email = "cm@gmail.com", Mobile = "+1 12457845", Picture = "", Status = "Blue", Level = 2, Remark = "Top performer", Strength = "Requirement Analysis", Weakness = "Flexibility", Gender = "F", IsFresher = false, OtherSkill = "Client Management" });
            defaultAssociates.Add(new Associate { Id = 100001, Name = "Scott Tanner", Email = "sct@ymail.com", Mobile = "+1 55664455", Picture = "", Status = "Green", Level = 2, Remark = "Good performer", Strength = "N/A", Weakness = "N/A", Gender = "M", IsFresher = false, OtherSkill = "N/A" });
            defaultAssociates.Add(new Associate { Id = 200002, Name = "John Terry", Email = "abc@gmail.com", Mobile = "+1 98765432", Picture = "", Status = "Red", Level = 3, Remark = "One to watch", Strength = "TBD", Weakness = "N/A", Gender = "M", IsFresher = true, OtherSkill = "Client Management" });

            context.Associates.AddRange(defaultAssociates);

            /*
            tbl_AssociateSkills
            */
            IList<AssociateSkill> defaultAssociateSkillMapping = new List<AssociateSkill>();

            defaultAssociateSkillMapping.Add(new AssociateSkill { AssociateId = 123456, SkillId = 1, Rating = 8 });
            defaultAssociateSkillMapping.Add(new AssociateSkill { AssociateId = 123456, SkillId = 2, Rating = 10 });
            defaultAssociateSkillMapping.Add(new AssociateSkill { AssociateId = 112334, SkillId = 4, Rating = 9 });
            defaultAssociateSkillMapping.Add(new AssociateSkill { AssociateId = 200002, SkillId = 6, Rating = 11 });
            defaultAssociateSkillMapping.Add(new AssociateSkill { AssociateId = 200002, SkillId = 1, Rating = 18 });
            defaultAssociateSkillMapping.Add(new AssociateSkill { AssociateId = 200002, SkillId = 8, Rating = 13 });

            context.AssociateSkills.AddRange(defaultAssociateSkillMapping);

            base.Seed(context);
        }
    }
}
