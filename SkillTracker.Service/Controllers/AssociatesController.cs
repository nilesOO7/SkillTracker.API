using System;
using System.Collections.Generic;
using System.Web.Http;
using AssociateTracker.Data.Provider.Implementation;
using SkillTracker.Data.Models;
using SkillTracker.Data.Provider.Contract;
using SkillTracker.Data.Provider.Implementation;
using SkillTracker.Data.Repository.Contract;
using SkillTracker.Data.Repository.Implementation;

namespace SkillTracker.Service.Controllers
{
    public class AssociatesController : ApiController
    {
        ISkillMasterProvider _skillProvider;
        ISkillMasterRepository _skillRepository;
        IAssociateProvider _associateProvider;
        IAssociateRepository _associateRepository;
        IDashboardProvider _dashboardProvider;
        IAssociateSkillMapRepository _assocSkillMapRepo;

        public AssociatesController()
            : this(null, null, null, null, null, null) { }

        [ExcludeFromCoverage]
        public AssociatesController(
            ISkillMasterRepository skillRepo,
            ISkillMasterProvider skillProvider,
            IAssociateRepository associateRepo,
            IAssociateProvider associateProvider,
            IAssociateSkillMapRepository assocSkillMapRepo,
            IDashboardProvider dashboardProvider)
        {
            if (skillRepo != null)
            {
                this._skillRepository = skillRepo;
            }
            else
            {
                this._skillRepository = new SkillMasterRepository();
            }

            if (skillProvider != null)
            {
                this._skillProvider = skillProvider;
            }
            else
            {
                this._skillProvider = new SkillMasterProvider(this._skillRepository);
            }

            if (associateRepo != null)
            {
                this._associateRepository = associateRepo;
            }
            else
            {
                this._associateRepository = new AssociateRepository();
            }

            if (associateProvider != null)
            {
                this._associateProvider = associateProvider;
            }
            else
            {
                this._associateProvider = new AssociateProvider(this._associateRepository);
            }

            if (assocSkillMapRepo != null)
            {
                this._assocSkillMapRepo = assocSkillMapRepo;
            }
            else
            {
                this._assocSkillMapRepo = new AssociateSkillMapRepository();
            }

            if (dashboardProvider != null)
            {
                this._dashboardProvider = dashboardProvider;
            }
            else
            {
                this._dashboardProvider = new DashboardProvider(this._skillRepository, this._associateRepository, this._assocSkillMapRepo);
            }
        }

        // GET: api/Associates
        public IHttpActionResult GetAssociates()
        {
            try
            {
                var result = this._associateProvider.GetAssociates();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // GET: api/Associates/5
        public IHttpActionResult GetAssociate(int id)
        {
            try
            {
                var currentAssociate = this._associateProvider.GetAssociate(id, false);

                if (currentAssociate != null)
                {
                    currentAssociate.PictureBase64String = Util.GetBase64StringForImage(currentAssociate.Picture);
                }

                return Ok(currentAssociate);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // POST: api/Associates
        public IHttpActionResult PostAssociate(Associate associate)
        {
            try
            {
                var result = this._associateProvider.AddAssociate(associate);

                if (result > 0)
                {
                    return Ok(result);
                }
                else return InternalServerError();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // PUT: api/Associates
        public IHttpActionResult PutAssociate(Associate associate)
        {
            try
            {
                var currentAssociate = this._associateProvider.GetAssociate(associate.Id, false);

                if (currentAssociate != null)
                {
                    var result = this._associateProvider.EditAssociate(associate);

                    if (result > 0)
                    {
                        return Ok(result);
                    }
                    else return InternalServerError();
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // DELETE: api/Associates/5
        public IHttpActionResult DeleteAssociate(int id)
        {
            try
            {
                var currentAssociate = this._associateProvider.GetAssociate(id, true);

                if (currentAssociate != null)
                {
                    var result = this._associateProvider.DeleteAssociate(currentAssociate);

                    if (result > 0)
                    {
                        return Ok(result);
                    }
                    else return InternalServerError();
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        [Route("api/Associates/Skills")]
        [HttpGet]
        public IHttpActionResult GetAssociateSkillDetails()
        {
            try
            {
                var result = this._dashboardProvider.GetAssociateSkillDetails();

                foreach (var record in result)
                {
                    record.PictureBase64String = Util.GetBase64StringForImage(record.Picture);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        [Route("api/Associates/{associateId}/Skills")]
        [HttpGet]
        public IHttpActionResult GetMappedSkillsOfAssociate(int associateId)
        {
            try
            {
                var result = this._dashboardProvider.GetAssociateSkillMap(associateId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        [Route("api/Associates/{associateId}/Skills")]
        [HttpPut]
        public IHttpActionResult MapSkillsToAssociate(int associateId, List<AssociateSkill> skillMap)
        {
            try
            {
                this._dashboardProvider.DeleteAssociateSkillMapping(associateId);

                if (skillMap != null && skillMap.Count > 0)
                {
                    this._dashboardProvider.AddAssociateSkillMapping(skillMap);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        [Route("api/Associates/Skills/Report")]
        [HttpGet]
        public IHttpActionResult GetSkillReport()
        {
            try
            {
                var result = this._dashboardProvider.GetAssociateSkillReport();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }
    }
}
