using System.Collections.Generic;
using SkillTracker.Data.Models;

namespace SkillTracker.Data.Provider.Contract
{
    public interface IDashboardProvider
    {
        List<Associate> GetAssociateSkillDetails();
        List<SkillMapForAssociate> GetAssociateSkillMap(int associateId);
        int AddAssociateSkillMapping(List<AssociateSkill> inputSkillMap);
        int DeleteAssociateSkillMapping(int associateId);
        Report GetAssociateSkillReport();
    }
}
