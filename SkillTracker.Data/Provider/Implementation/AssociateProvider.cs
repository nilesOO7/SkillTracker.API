using System.Collections.Generic;
using SkillTracker.Data.Models;
using SkillTracker.Data.Provider.Contract;
using SkillTracker.Data.Repository.Contract;
using SkillTracker.Data.Repository.Implementation;

namespace AssociateTracker.Data.Provider.Implementation
{
    public class AssociateProvider : IAssociateProvider
    {
        private IAssociateRepository _associateRepo;
        
        [ExcludeFromCoverage]
        public AssociateProvider(IAssociateRepository associateRepo)
        {
            if (associateRepo != null)
            {
                this._associateRepo = associateRepo;
            }
            else
            {
                this._associateRepo = new AssociateRepository();
            }
        }

        public List<Associate> GetAssociates()
        {
            return this._associateRepo.GetAssociates();
        }

        public Associate GetAssociate(int associateId, bool preserveState)
        {
            return this._associateRepo.GetAssociate(associateId, preserveState);
        }

        public int AddAssociate(Associate inputAssociate)
        {
            return this._associateRepo.AddAssociate(inputAssociate);
        }

        public int EditAssociate(Associate inputAssociate)
        {
            return this._associateRepo.EditAssociate(inputAssociate);
        }

        public int DeleteAssociate(Associate inputAssociate)
        {
            return this._associateRepo.DeleteAssociate(inputAssociate);
        }
    }
}
