using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using NUnit.Framework;
using SkillTracker.Data.Models;
using SkillTracker.Service.Controllers;

namespace AssociateTracker.Tests
{
    [TestFixture]
    public class AssociatesControllerTest
    {
        private SkillsController _skillController;
        private AssociatesController _associateController;

        public AssociatesControllerTest()
        {
            this._skillController = new SkillsController();
            this._associateController = new AssociatesController();
        }

        [Test]
        [Category("AssociatesControllerTest")]
        public void TestMethod_GetAllAssociates_Success()
        {
            var getAllResponse = this._associateController.GetAssociates();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as OkNegotiatedContentResult<List<Associate>>;
            Assert.IsNotNull(getAllResult);
            Assert.IsNotNull(getAllResult.Content);
            Assert.GreaterOrEqual(getAllResult.Content.Count, 0);
            //Assert.IsFalse(getAllResult.Content.FirstOrDefault(r => r.Name == "Clara Matthew").IsFresher);
            //Assert.AreEqual(getAllResult.Content.FirstOrDefault(r => r.Email == "sam@ymail.com").Weakness, "Team Building");
        }

        ////[Test]
        ////[Category("AssociatesControllerTest")]
        ////public void TestMethod_GetAssociate_Success()
        ////{
        ////    var getResponse = this._associateController.GetAssociate(112334);
        ////    Assert.IsNotNull(getResponse);

        ////    var getResult = getResponse as OkNegotiatedContentResult<Associate>;
        ////    Assert.IsNotNull(getResult);
        ////    Assert.IsNotNull(getResult.Content);
        ////    Assert.AreEqual(getResult.Content.Strength, "Requirement Analysis");
        ////    Assert.AreEqual(getResult.Content.Status, "Blue");
        ////}

        [Test]
        [Category("AssociatesControllerTest")]
        public void TestMethod_PostAssociate_Success()
        {
            Associate associate = new Associate { Id = -1, Name = "UnitTest_PostAssociate_Success", Email = "test@ymail.com", Mobile = "+1 11111111", Picture = "", Status = "Green", Level = 1, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "M", IsFresher = false, OtherSkill = "CSR Related Works" };
            var postResponse = this._associateController.PostAssociate(associate);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult);
            Assert.IsNotNull(postResult.Content);
            Assert.AreEqual(postResult.Content, 1);

            var getAllResponse = this._associateController.GetAssociates();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as OkNegotiatedContentResult<List<Associate>>;
            Assert.IsNotNull(getAllResult);
            Assert.IsNotNull(getAllResult.Content);
            Assert.GreaterOrEqual(getAllResult.Content.Where(w => w.Name == "UnitTest_PostAssociate_Success").Count(), 1);

            //// CleanUp
            this._associateController.DeleteAssociate(-1);
        }

        [Test]
        [Category("AssociatesControllerTest")]
        public void TestMethod_PutAssociate_Success()
        {
            Associate associate = new Associate { Id = -2, Name = "UnitTest_PutAssociate_Success", Email = "test@ymail.com", Mobile = "+1 11111111", Picture = "", Status = "Green", Level = 1, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "M", IsFresher = false, OtherSkill = "CSR Related Works" };
            var postResponse = this._associateController.PostAssociate(associate);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult);
            Assert.IsNotNull(postResult.Content);
            Assert.AreEqual(postResult.Content, 1);

            var getAllResponse = this._associateController.GetAssociates();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as OkNegotiatedContentResult<List<Associate>>;
            Assert.IsNotNull(getAllResult);
            Assert.IsNotNull(getAllResult.Content);
            Assert.GreaterOrEqual(getAllResult.Content.Where(w => w.Name == "UnitTest_PutAssociate_Success").Count(), 1);

            var associateToUpdate = getAllResult.Content.Where(w => w.Name == "UnitTest_PutAssociate_Success").FirstOrDefault();
            associateToUpdate.Name = "UnitTest_PutAssociate_Updated_Success";
            var putResponse = this._associateController.PutAssociate(associateToUpdate);
            Assert.IsNotNull(putResponse);

            var putResult = putResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(putResult);
            Assert.IsNotNull(putResult.Content);
            Assert.AreEqual(putResult.Content, 1);

            var getResponse = this._associateController.GetAssociate(associateToUpdate.Id);
            Assert.IsNotNull(getResponse);

            var getResult = getResponse as OkNegotiatedContentResult<Associate>;
            Assert.IsNotNull(getResult);
            Assert.IsNotNull(getResult.Content);
            Assert.AreEqual(getResult.Content.Name, "UnitTest_PutAssociate_Updated_Success");

            //// CleanUp
            this._associateController.DeleteAssociate(-2);
        }

        [Test]
        [Category("AssociatesControllerTest")]
        public void TestMethod_DeleteAssociate_Success()
        {
            Associate associate = new Associate { Id = -3, Name = "UnitTest_DeleteAssociate_Success", Email = "test@ymail.com", Mobile = "+1 11111111", Picture = "", Status = "Green", Level = 1, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "M", IsFresher = false, OtherSkill = "CSR Related Works" };
            var postResponse = this._associateController.PostAssociate(associate);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult);
            Assert.IsNotNull(postResult.Content);
            Assert.AreEqual(postResult.Content, 1);

            var getAllResponse = this._associateController.GetAssociates();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as OkNegotiatedContentResult<List<Associate>>;
            Assert.IsNotNull(getAllResult);
            Assert.IsNotNull(getAllResult.Content);
            Assert.GreaterOrEqual(getAllResult.Content.Where(w => w.Name == "UnitTest_DeleteAssociate_Success").Count(), 1);

            var associateToDelete = getAllResult.Content.Where(w => w.Name == "UnitTest_DeleteAssociate_Success").FirstOrDefault();
            var deleteResponse = this._associateController.DeleteAssociate(associateToDelete.Id);
            Assert.IsNotNull(deleteResponse);

            var deleteResult = deleteResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(deleteResult);
            Assert.IsNotNull(deleteResult.Content);
            Assert.AreEqual(deleteResult.Content, 1);

            var getResponse = this._associateController.GetAssociate(associateToDelete.Id);
            Assert.IsNotNull(getResponse);

            var getResult = getResponse as OkNegotiatedContentResult<object>;
            Assert.IsNull(getResult);
        }

        [Test]
        [Category("AssociatesControllerTest")]
        public void TestMethod_GetAssociateSkillDetails_Success()
        {
            var getAllResponse = this._associateController.GetAssociateSkillDetails();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as OkNegotiatedContentResult<List<Associate>>;
            Assert.IsNotNull(getAllResult);
            Assert.IsNotNull(getAllResult.Content);
            Assert.GreaterOrEqual(getAllResult.Content.Count, 0);
            //Assert.AreEqual(getAllResult.Content.FirstOrDefault(r => r.Name == "Clara Matthew").Status, "Blue");
            //Assert.AreEqual(getAllResult.Content.FirstOrDefault(r => r.Email == "sam@ymail.com").Mobile, "+1 89787878");
            //Assert.AreEqual(getAllResult.Content.FirstOrDefault(r => r.Id == 200002).StrongSkills, "HTML5, Angular 1, React");
            //Assert.AreEqual(getAllResult.Content.FirstOrDefault(r => r.Email == "sct@ymail.com").StrongSkills, "");
        }

        ////[Test]
        ////[Category("AssociatesControllerTest")]
        ////public void TestMethod_GetMappedSkillsOfAssociate_Success()
        ////{
        ////    var getAllResponse = this._associateController.GetMappedSkillsOfAssociate(123456);
        ////    Assert.IsNotNull(getAllResponse);

        ////    var getAllResult = getAllResponse as OkNegotiatedContentResult<List<SkillMapForAssociate>>;
        ////    Assert.IsNotNull(getAllResult);
        ////    Assert.IsNotNull(getAllResult.Content);
        ////    Assert.GreaterOrEqual(getAllResult.Content.Count, 8);
        ////    Assert.GreaterOrEqual(getAllResult.Content.Where(w => w.IsSelected).Count(), 2);
        ////    Assert.GreaterOrEqual(getAllResult.Content.Where(w => w.Name == "HTML5").Count(), 1);
        ////}

        [Test]
        [Category("AssociatesControllerTest")]
        public void TestMethod_MapSkillsToAssociate_Success()
        {
            //// Add Skills First
            Skill skill1 = new Skill { Id = -10, Name = "UnitTest_MapSkill_Success_1", IsTechnical = true };
            var postResponse_skill1 = this._skillController.PostSkill(skill1);
            Assert.IsNotNull(postResponse_skill1);
            var postResult_skill1 = postResponse_skill1 as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult_skill1);
            Assert.IsNotNull(postResult_skill1.Content);
            Assert.GreaterOrEqual(postResult_skill1.Content, 1);
            int skill1_id = postResult_skill1.Content;

            Skill skill2 = new Skill { Id = -11, Name = "UnitTest_MapSkill_Success_2", IsTechnical = true };
            var postResponse_skill2 = this._skillController.PostSkill(skill2);
            Assert.IsNotNull(postResponse_skill2);
            var postResult_skill2 = postResponse_skill2 as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult_skill2);
            Assert.IsNotNull(postResult_skill2.Content);
            Assert.GreaterOrEqual(postResult_skill2.Content, 1);
            int skill2_id = postResult_skill2.Content;

            Skill skill3 = new Skill { Id = -12, Name = "UnitTest_MapSkill_Success_3", IsTechnical = false };
            var postResponse_skill3 = this._skillController.PostSkill(skill3);
            Assert.IsNotNull(postResponse_skill3);
            var postResult_skill3 = postResponse_skill3 as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult_skill3);
            Assert.IsNotNull(postResult_skill3.Content);
            Assert.GreaterOrEqual(postResult_skill3.Content, 1);
            int skill3_id = postResult_skill3.Content;

            //// Add Associate
            Associate associate = new Associate { Id = -4, Name = "UnitTest_MapSkillsToAssociate", Email = "test@ymail.com", Mobile = "+1 11111111", Picture = "", Status = "Green", Level = 1, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "M", IsFresher = false, OtherSkill = "CSR Related Works" };
            var postResponse = this._associateController.PostAssociate(associate);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult);
            Assert.IsNotNull(postResult.Content);
            Assert.AreEqual(postResult.Content, 1);

            //// Mao Skills to Associate
            var mapResponse = this._associateController.MapSkillsToAssociate(-4, new List<AssociateSkill>
            {
                new AssociateSkill { AssociateId = -4, SkillId = skill1_id, Rating = 10},
                new AssociateSkill { AssociateId = -4, SkillId = skill2_id, Rating = 20}
            });

            Assert.IsNotNull(mapResponse);

            var mapResult = mapResponse as OkNegotiatedContentResult<bool>;
            Assert.IsNotNull(mapResult);
            Assert.IsNotNull(mapResult.Content);
            Assert.AreEqual(mapResult.Content, true);

            var getAllResponse = this._associateController.GetMappedSkillsOfAssociate(-4);
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as OkNegotiatedContentResult<List<SkillMapForAssociate>>;
            Assert.IsNotNull(getAllResult);
            Assert.IsNotNull(getAllResult.Content);
            Assert.GreaterOrEqual(getAllResult.Content.Count, 3);
            Assert.AreEqual(getAllResult.Content.Where(w => w.IsSelected).Count(), 2);
            Assert.AreEqual(getAllResult.Content.Where(w => w.Name == "UnitTest_MapSkill_Success_1" && w.IsSelected).Count(), 1);
            Assert.AreEqual(getAllResult.Content.Where(w => w.Name == "UnitTest_MapSkill_Success_2" && w.IsSelected).Count(), 1);

            //// Update
            var mapResponse2 = this._associateController.MapSkillsToAssociate(-4, new List<AssociateSkill>
            {
                new AssociateSkill { AssociateId = -4, SkillId = skill2_id, Rating = 6},
                new AssociateSkill { AssociateId = -4, SkillId = skill3_id, Rating = 13}
            });

            Assert.IsNotNull(mapResponse2);

            var mapResult2 = mapResponse2 as OkNegotiatedContentResult<bool>;
            Assert.IsNotNull(mapResult2);
            Assert.IsNotNull(mapResult2.Content);
            Assert.AreEqual(mapResult2.Content, true);

            var getAllResponse2 = this._associateController.GetMappedSkillsOfAssociate(-4);
            Assert.IsNotNull(getAllResponse2);

            var getAllResult2 = getAllResponse2 as OkNegotiatedContentResult<List<SkillMapForAssociate>>;
            Assert.IsNotNull(getAllResult2);
            Assert.IsNotNull(getAllResult2.Content);
            Assert.GreaterOrEqual(getAllResult2.Content.Count, 3);
            Assert.AreEqual(getAllResult2.Content.Where(w => w.IsSelected).Count(), 2);
            Assert.AreEqual(getAllResult2.Content.Where(w => w.Name == "UnitTest_MapSkill_Success_1" && w.IsSelected).Count(), 0);
            Assert.AreEqual(getAllResult2.Content.Where(w => w.Name == "UnitTest_MapSkill_Success_2" && w.IsSelected).Count(), 1);
            Assert.AreEqual(getAllResult2.Content.Where(w => w.Name == "UnitTest_MapSkill_Success_3" && w.IsSelected).Count(), 1);
            Assert.AreEqual(getAllResult2.Content.Where(w => w.Name == "UnitTest_MapSkill_Success_2" && w.IsSelected).FirstOrDefault().Rating, 6);
            Assert.AreEqual(getAllResult2.Content.Where(w => w.Name == "UnitTest_MapSkill_Success_3" && w.IsSelected).FirstOrDefault().IsTechnical, false);

            //// Delete
            var mapResponse3 = this._associateController.MapSkillsToAssociate(-4, new List<AssociateSkill>());

            Assert.IsNotNull(mapResponse3);

            var mapResult3 = mapResponse3 as OkNegotiatedContentResult<bool>;
            Assert.IsNotNull(mapResult3);
            Assert.IsNotNull(mapResult3.Content);
            Assert.AreEqual(mapResult3.Content, true);

            var getAllResponse3 = this._associateController.GetMappedSkillsOfAssociate(-4);
            Assert.IsNotNull(getAllResponse3);

            var getAllResult3 = getAllResponse3 as OkNegotiatedContentResult<List<SkillMapForAssociate>>;
            Assert.IsNotNull(getAllResult3);
            Assert.IsNotNull(getAllResult3.Content);
            Assert.GreaterOrEqual(getAllResult3.Content.Count, 3);
            Assert.AreEqual(getAllResult3.Content.Where(w => w.IsSelected).Count(), 0);

            //// CleanUp
            this._skillController.DeleteSkill(skill1_id);
            this._skillController.DeleteSkill(skill2_id);
            this._skillController.DeleteSkill(skill3_id);
            this._associateController.DeleteAssociate(-4);
        }

        [Test]
        [Category("AssociatesControllerTest")]
        public void TestMethod_GetSkillReport_Success()
        {
            //// Add Skills First
            Skill skill1 = new Skill { Id = -101, Name = "UnitTest_GetSkillReport_Success_1", IsTechnical = true };
            var postResponse_skill1 = this._skillController.PostSkill(skill1);
            Assert.IsNotNull(postResponse_skill1);
            var postResult_skill1 = postResponse_skill1 as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult_skill1);
            Assert.IsNotNull(postResult_skill1.Content);
            Assert.GreaterOrEqual(postResult_skill1.Content, 1);
            int skill1_id = postResult_skill1.Content;

            Skill skill2 = new Skill { Id = -102, Name = "UnitTest_GetSkillReport_Success_2", IsTechnical = true };
            var postResponse_skill2 = this._skillController.PostSkill(skill2);
            Assert.IsNotNull(postResponse_skill2);
            var postResult_skill2 = postResponse_skill2 as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult_skill2);
            Assert.IsNotNull(postResult_skill2.Content);
            Assert.GreaterOrEqual(postResult_skill2.Content, 1);
            int skill2_id = postResult_skill2.Content;

            Skill skill3 = new Skill { Id = -103, Name = "UnitTest_GetSkillReport_Success_3", IsTechnical = false };
            var postResponse_skill3 = this._skillController.PostSkill(skill3);
            Assert.IsNotNull(postResponse_skill3);
            var postResult_skill3 = postResponse_skill3 as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult_skill3);
            Assert.IsNotNull(postResult_skill3.Content);
            Assert.GreaterOrEqual(postResult_skill3.Content, 1);
            int skill3_id = postResult_skill3.Content;

            //// Add Associate
            Associate associate = new Associate { Id = -101, Name = "UnitTest_GetSkillReport", Email = "test@ymail.com", Mobile = "+1 11111111", Picture = "", Status = "Red", Level = 1, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "M", IsFresher = false, OtherSkill = "CSR Related Works" };
            var postResponse = this._associateController.PostAssociate(associate);
            Assert.IsNotNull(postResponse);
            var postResult = postResponse as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult);
            Assert.IsNotNull(postResult.Content);
            Assert.AreEqual(postResult.Content, 1);

            Associate associate2 = new Associate { Id = -102, Name = "UnitTest_GetSkillReport2", Email = "test@ymail.com", Mobile = "+1 11111111", Picture = "", Status = "Green", Level = 2, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "M", IsFresher = true, OtherSkill = "CSR Related Works" };
            var postResponse2 = this._associateController.PostAssociate(associate2);
            Assert.IsNotNull(postResponse2);
            var postResult2 = postResponse2 as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult2);
            Assert.IsNotNull(postResult2.Content);
            Assert.AreEqual(postResult2.Content, 1);

            Associate associate3 = new Associate { Id = -103, Name = "UnitTest_GetSkillReport3", Email = "test@ymail.com", Mobile = "+1 11111111", Picture = "", Status = "Blue", Level = 1, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "F", IsFresher = false, OtherSkill = "CSR Related Works" };
            var postResponse3 = this._associateController.PostAssociate(associate3);
            Assert.IsNotNull(postResponse3);
            var postResult3 = postResponse3 as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(postResult3);
            Assert.IsNotNull(postResult3.Content);
            Assert.AreEqual(postResult3.Content, 1);

            //// Mao Skills to Associate
            var mapResponse = this._associateController.MapSkillsToAssociate(-4, new List<AssociateSkill>
            {
                new AssociateSkill { AssociateId = -101, SkillId = skill1_id, Rating = 14},
                new AssociateSkill { AssociateId = -101, SkillId = skill2_id, Rating = 9}
            });
            Assert.IsNotNull(mapResponse);
            var mapResult = mapResponse as OkNegotiatedContentResult<bool>;
            Assert.IsNotNull(mapResult);
            Assert.IsNotNull(mapResult.Content);
            Assert.AreEqual(mapResult.Content, true);

            var mapResponse2 = this._associateController.MapSkillsToAssociate(-4, new List<AssociateSkill>
            {
                new AssociateSkill { AssociateId = -102, SkillId = skill2_id, Rating = 11},
                new AssociateSkill { AssociateId = -102, SkillId = skill3_id, Rating = 13}
            });
            Assert.IsNotNull(mapResponse2);
            var mapResult2 = mapResponse2 as OkNegotiatedContentResult<bool>;
            Assert.IsNotNull(mapResult2);
            Assert.IsNotNull(mapResult2.Content);
            Assert.AreEqual(mapResult2.Content, true);

            //// Get Skill Report
            var getAllResponse = this._associateController.GetSkillReport();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as OkNegotiatedContentResult<Report>;
            Assert.IsNotNull(getAllResult);
            Assert.IsNotNull(getAllResult.Content);

            Assert.GreaterOrEqual(getAllResult.Content.RegisteredCandidateCount, 3);
            Assert.GreaterOrEqual(getAllResult.Content.RatedCandidateCount, 2);

            Assert.Greater(getAllResult.Content.FemaleCandidatePercentage, 0);
            Assert.Greater(getAllResult.Content.FresherCandidatePercentage, 0);
            Assert.Greater(getAllResult.Content.Level1CandidatePercentage, 0);

            //// CleanUp
            this._associateController.DeleteAssociate(-101);
            this._associateController.DeleteAssociate(-102);
            this._associateController.DeleteAssociate(-103);
            this._skillController.DeleteSkill(skill1_id);
            this._skillController.DeleteSkill(skill2_id);
            this._skillController.DeleteSkill(skill3_id);
        }
    }
}
