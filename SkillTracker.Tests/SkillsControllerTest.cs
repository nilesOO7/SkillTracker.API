using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using NUnit.Framework;
using SkillTracker.Data.Models;
using SkillTracker.Service.Controllers;

namespace SkillTracker.Tests
{
    [TestFixture]
    public class SkillsControllerTest
    {
        private SkillsController _skillController;

        public SkillsControllerTest()
        {
            this._skillController = new SkillsController();
        }

        ////[Test]
        ////[Category("SkillsControllerTest")]
        ////public void TestMethod_GetAllSkills_Success()
        ////{
        ////    var getAllResponse = this._skillController.GetSkills();
        ////    Assert.IsNotNull(getAllResponse);

        ////    var getAllResult = getAllResponse as OkNegotiatedContentResult<List<Skill>>;
        ////    Assert.IsNotNull(getAllResult);
        ////    Assert.IsNotNull(getAllResult.Content);
        ////    Assert.GreaterOrEqual(getAllResult.Content.Count, 5);
        ////    Assert.IsFalse(getAllResult.Content.FirstOrDefault(r => r.Name == "Aptitude").IsTechnical);
        ////    Assert.AreEqual(getAllResult.Content.FirstOrDefault(r => r.Name == "CSS3").Id, 2);
        ////}

        ////[Test]
        ////[Category("SkillsControllerTest")]
        ////public void TestMethod_GetSkill_Success()
        ////{
        ////    var getResponse = this._skillController.GetSkill(1);
        ////    Assert.IsNotNull(getResponse);

        ////    var getResult = getResponse as OkNegotiatedContentResult<Skill>;
        ////    Assert.IsNotNull(getResult);
        ////    Assert.IsNotNull(getResult.Content);
        ////    Assert.AreEqual(getResult.Content.Name, "HTML5");
        ////}

        [Test]
        [Category("SkillsControllerTest")]
        public void TestMethod_PostSkill_Success()
        {
            Skill skill = new Skill { Id = -1, Name = "UnitTest_PostSkill_Success", IsTechnical = false };
            var postResponse = this._skillController.PostSkill(skill);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult);
            Assert.IsNotNull(postResult.Content);
            Assert.GreaterOrEqual(postResult.Content, 1);

            var getAllResponse = this._skillController.GetSkills();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as OkNegotiatedContentResult<List<Skill>>;
            Assert.IsNotNull(getAllResult);
            Assert.IsNotNull(getAllResult.Content);
            Assert.GreaterOrEqual(getAllResult.Content.Where(w => w.Name == "UnitTest_PostSkill_Success").Count(), 1);

            //// CleanUp
            // Clear SkillsController Test Data If Any
            ((OkNegotiatedContentResult<List<Skill>>)this._skillController.GetSkills())
                .Content
                .Where(w => w.Name.StartsWith("UnitTest_PostSkill_Success", StringComparison.OrdinalIgnoreCase))
                .ToList()
                .ForEach(itemToDelete =>
                {
                    this._skillController.DeleteSkill(itemToDelete.Id);
                });
        }

        [Test]
        [Category("SkillsControllerTest")]
        public void TestMethod_PutSkill_Success()
        {
            Skill skill = new Skill { Id = -1, Name = "UnitTest_PutSkill_Success", IsTechnical = false };
            var postResponse = this._skillController.PostSkill(skill);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult);
            Assert.IsNotNull(postResult.Content);
            Assert.GreaterOrEqual(postResult.Content, 1);

            var getAllResponse = this._skillController.GetSkills();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as OkNegotiatedContentResult<List<Skill>>;
            Assert.IsNotNull(getAllResult);
            Assert.IsNotNull(getAllResult.Content);
            Assert.GreaterOrEqual(getAllResult.Content.Where(w => w.Name == "UnitTest_PutSkill_Success").Count(), 1);

            var skillToUpdate = getAllResult.Content.Where(w => w.Name == "UnitTest_PutSkill_Success").FirstOrDefault();
            skillToUpdate.Name = "UnitTest_PutSkill_Updated_Success";
            var putResponse = this._skillController.PutSkill(skillToUpdate);
            Assert.IsNotNull(putResponse);

            var putResult = putResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(putResult);
            Assert.IsNotNull(putResult.Content);
            Assert.AreEqual(putResult.Content, 1);

            var getResponse = this._skillController.GetSkill(skillToUpdate.Id);
            Assert.IsNotNull(getResponse);

            var getResult = getResponse as OkNegotiatedContentResult<Skill>;
            Assert.IsNotNull(getResult);
            Assert.IsNotNull(getResult.Content);
            Assert.AreEqual(getResult.Content.Name, "UnitTest_PutSkill_Updated_Success");

            //// CleanUp
            // Clear SkillsController Test Data If Any
            ((OkNegotiatedContentResult<List<Skill>>)this._skillController.GetSkills())
                .Content
                .Where(w => w.Name.StartsWith("UnitTest_PutSkill_Updated_Success", StringComparison.OrdinalIgnoreCase))
                .ToList()
                .ForEach(itemToDelete =>
                {
                    this._skillController.DeleteSkill(itemToDelete.Id);
                });
        }

        [Test]
        [Category("SkillsControllerTest")]
        public void TestMethod_DeleteSkill_Success()
        {
            Skill skill = new Skill { Id = -1, Name = "UnitTest_DeleteSkill_Success", IsTechnical = false };
            var postResponse = this._skillController.PostSkill(skill);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult);
            Assert.IsNotNull(postResult.Content);
            Assert.GreaterOrEqual(postResult.Content, 1);

            var getAllResponse = this._skillController.GetSkills();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as OkNegotiatedContentResult<List<Skill>>;
            Assert.IsNotNull(getAllResult);
            Assert.IsNotNull(getAllResult.Content);
            Assert.GreaterOrEqual(getAllResult.Content.Where(w => w.Name == "UnitTest_DeleteSkill_Success").Count(), 1);

            var skillToDelete = getAllResult.Content.Where(w => w.Name == "UnitTest_DeleteSkill_Success").FirstOrDefault();
            var deleteResponse = this._skillController.DeleteSkill(skillToDelete.Id);
            Assert.IsNotNull(deleteResponse);

            var deleteResult = deleteResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(deleteResult);
            Assert.IsNotNull(deleteResult.Content);
            Assert.AreEqual(deleteResult.Content, 1);

            var getResponse = this._skillController.GetSkill(skillToDelete.Id);
            Assert.IsNotNull(getResponse);

            var getResult = getResponse as NotFoundResult;
            Assert.IsNotNull(getResult);
        }
    }
}
