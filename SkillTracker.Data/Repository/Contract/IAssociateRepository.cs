using System.Collections.Generic;
using SkillTracker.Data.Models;

namespace SkillTracker.Data.Repository.Contract
{
    public interface IAssociateRepository
    {
        List<Associate> GetAssociates();
        Associate GetAssociate(int associateId, bool preserveState);
        int AddAssociate(Associate inputAssociate);
        int EditAssociate(Associate inputAssociate);
        int DeleteAssociate(Associate inputAssociate);
    }
}
