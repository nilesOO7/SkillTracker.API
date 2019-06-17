using System;
using System.Web.Http;
using SkillTracker.Data.Models;
using SkillTracker.Data.Provider.Contract;
using SkillTracker.Data.Provider.Implementation;
using SkillTracker.Data.Repository.Contract;
using SkillTracker.Data.Repository.Implementation;

namespace SkillTracker.Service.Controllers
{
    public class SkillsController : ApiController
    {
        ISkillMasterProvider _skillProvider;
        ISkillMasterRepository _skillRepository;

        public SkillsController()
            : this(null, null) { }

        [ExcludeFromCoverage]
        public SkillsController(ISkillMasterRepository skillRepo, ISkillMasterProvider skillProvider)
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
        }

        // GET: api/Skills
        public IHttpActionResult GetSkills()
        {
            try
            {
                var result = this._skillProvider.GetSkills();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // GET: api/Skills/5
        public IHttpActionResult GetSkill(int id)
        {
            try
            {
                var currentSkill = this._skillProvider.GetSkill(id, false);

                if (currentSkill != null)
                    return Ok(currentSkill);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // POST: api/Skills
        public IHttpActionResult PostSkill(Skill skill)
        {
            try
            {
                var result = this._skillProvider.AddSkill(skill);

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

        // PUT: api/Skills
        public IHttpActionResult PutSkill(Skill skill)
        {
            try
            {
                var currentSkill = this._skillProvider.GetSkill(skill.Id, false);

                if (currentSkill != null)
                {
                    var result = this._skillProvider.EditSkill(skill);

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

        // DELETE: api/Skills/5
        public IHttpActionResult DeleteSkill(int id)
        {
            try
            {
                var currentSkill = this._skillProvider.GetSkill(id, true);

                if (currentSkill != null)
                {
                    var result = this._skillProvider.DeleteSkill(currentSkill);

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
    }
}
