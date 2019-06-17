using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using NUnit.Framework;
using SkillTracker.Data.Provider.Contract;
using SkillTracker.Service.Controllers;
using SkillTracker.Data.Models;
using Moq;

namespace AssociateTracker.Tests
{
    [TestFixture]
    public class AssociatesControllerMockTest
    {
        private SkillsController _skillController;
        private AssociatesController _associateController;

        public AssociatesControllerMockTest()
        {
            this._skillController = new SkillsController();
            this._associateController = new AssociatesController();
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_GetAllAssociates_Exception()
        {
            var _mockAssociateProvider = new Mock<IAssociateProvider>();
            _mockAssociateProvider
                .Setup(s => s.GetAssociates())
                .Throws(new Exception("Mock Unit Test Exception"));

            this._associateController = new AssociatesController(null, null, null, _mockAssociateProvider.Object, null, null);

            var getAllResponse = this._associateController.GetAssociates();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as InternalServerErrorResult;
            Assert.IsNotNull(getAllResult);
        }

        ////[Test]
        ////[Category("AssociatesControllerMockTest")]
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
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_PostAssociate_Exception()
        {
            var _mockAssociateProvider = new Mock<IAssociateProvider>();
            _mockAssociateProvider
                .Setup(s => s.AddAssociate(It.IsAny<Associate>()))
                .Throws(new Exception("Mock Unit Test Exception"));

            this._associateController = new AssociatesController(null, null, null, _mockAssociateProvider.Object, null, null);

            Associate associate = new Associate { Id = -1, Name = "UnitTest_PostAssociate_Exception", Email = "test@ymail.com", Mobile = "+1 11111111", Picture = "", Status = "Green", Level = 1, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "M", IsFresher = false, OtherSkill = "CSR Related Works" };
            var postResponse = this._associateController.PostAssociate(associate);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as InternalServerErrorResult;
            Assert.IsNotNull(postResult);
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_PostAssociate_Failure()
        {
            var _mockAssociateProvider = new Mock<IAssociateProvider>();
            _mockAssociateProvider
                .Setup(s => s.AddAssociate(It.IsAny<Associate>()))
                .Returns(() => { return 0; });

            this._associateController = new AssociatesController(null, null, null, _mockAssociateProvider.Object, null, null);

            Associate associate = new Associate { Id = -1, Name = "UnitTest_PostAssociate_Failure", Email = "test@ymail.com", Mobile = "+1 11111111", Picture = "", Status = "Green", Level = 1, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "M", IsFresher = false, OtherSkill = "CSR Related Works" };
            var postResponse = this._associateController.PostAssociate(associate);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as InternalServerErrorResult;
            Assert.IsNotNull(postResult);
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_PutAssociate_NotFound()
        {
            var _mockAssociateProvider = new Mock<IAssociateProvider>();
            _mockAssociateProvider
                .Setup(s => s.GetAssociate(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return null; });

            this._associateController = new AssociatesController(null, null, null, _mockAssociateProvider.Object, null, null);

            var assocToUpdate = new Associate { Id = 123, Name = "UnitTest_PutAssociate_NotFound" };
            var putResponse = this._associateController.PutAssociate(assocToUpdate);
            Assert.IsNotNull(putResponse);

            var putResult = putResponse as NotFoundResult;
            Assert.IsNotNull(putResult);
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_PutAssociate_Exception()
        {
            var _mockAssociateProvider = new Mock<IAssociateProvider>();
            _mockAssociateProvider
                .Setup(s => s.GetAssociate(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return new Associate(); });
            _mockAssociateProvider
                .Setup(s => s.EditAssociate(It.IsAny<Associate>()))
                .Throws(new Exception("Mock Unit Test Exception"));

            this._associateController = new AssociatesController(null, null, null, _mockAssociateProvider.Object, null, null);

            var assocToUpdate = new Associate { Id = 123, Name = "UnitTest_PutAssociate_Exception" };
            var putResponse = this._associateController.PutAssociate(assocToUpdate);
            Assert.IsNotNull(putResponse);

            var putResult = putResponse as InternalServerErrorResult;
            Assert.IsNotNull(putResult);
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_PutAssociate_Failure()
        {
            var _mockAssociateProvider = new Mock<IAssociateProvider>();
            _mockAssociateProvider
                .Setup(s => s.GetAssociate(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return new Associate(); });
            _mockAssociateProvider
                .Setup(s => s.EditAssociate(It.IsAny<Associate>()))
                .Returns(() => { return 0; });

            this._associateController = new AssociatesController(null, null, null, _mockAssociateProvider.Object, null, null);

            var assocToUpdate = new Associate { Id = 123, Name = "UnitTest_PutAssociate_Failure" };
            var putResponse = this._associateController.PutAssociate(assocToUpdate);
            Assert.IsNotNull(putResponse);

            var putResult = putResponse as InternalServerErrorResult;
            Assert.IsNotNull(putResult);
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_DeleteAssociate_NotFound()
        {
            var _mockAssociateProvider = new Mock<IAssociateProvider>();
            _mockAssociateProvider
                .Setup(s => s.GetAssociate(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return null; });

            this._associateController = new AssociatesController(null, null, null, _mockAssociateProvider.Object, null, null);

            var delResponse = this._associateController.DeleteAssociate(-1);
            Assert.IsNotNull(delResponse);

            var delResult = delResponse as NotFoundResult;
            Assert.IsNotNull(delResult);
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_DeleteAssociate_Exception()
        {
            var _mockAssociateProvider = new Mock<IAssociateProvider>();
            _mockAssociateProvider
                .Setup(s => s.GetAssociate(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return new Associate(); });
            _mockAssociateProvider
                .Setup(s => s.DeleteAssociate(It.IsAny<Associate>()))
                .Throws(new Exception("Mock Unit Test Exception"));

            this._associateController = new AssociatesController(null, null, null, _mockAssociateProvider.Object, null, null);

            var delResponse = this._associateController.DeleteAssociate(-1);
            Assert.IsNotNull(delResponse);

            var delResult = delResponse as InternalServerErrorResult;
            Assert.IsNotNull(delResult);
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_DeleteAssociate_Failure()
        {
            var _mockAssociateProvider = new Mock<IAssociateProvider>();
            _mockAssociateProvider
                .Setup(s => s.GetAssociate(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return new Associate(); });
            _mockAssociateProvider
                .Setup(s => s.DeleteAssociate(It.IsAny<Associate>()))
                .Returns(() => { return 0; });

            this._associateController = new AssociatesController(null, null, null, _mockAssociateProvider.Object, null, null);

            var delResponse = this._associateController.DeleteAssociate(-1);
            Assert.IsNotNull(delResponse);

            var delResult = delResponse as InternalServerErrorResult;
            Assert.IsNotNull(delResult);
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_GetAssociateSkillDetails_Exception()
        {
            var _mockDashboardProvider = new Mock<IDashboardProvider>();
            _mockDashboardProvider
                .Setup(s => s.GetAssociateSkillDetails())
                .Throws(new Exception("Mock Unit Test Exception"));

            this._associateController = new AssociatesController(null, null, null, null, null, _mockDashboardProvider.Object);

            var getAllResponse = this._associateController.GetAssociateSkillDetails();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as InternalServerErrorResult;
            Assert.IsNotNull(getAllResult);
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_GetMappedSkillsOfAssociate_Exception()
        {
            var _mockDashboardProvider = new Mock<IDashboardProvider>();
            _mockDashboardProvider
                .Setup(s => s.GetAssociateSkillMap(It.IsAny<int>()))
                .Throws(new Exception("Mock Unit Test Exception"));

            this._associateController = new AssociatesController(null, null, null, null, null, _mockDashboardProvider.Object);

            var getAllResponse = this._associateController.GetMappedSkillsOfAssociate(123456);
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as InternalServerErrorResult;
            Assert.IsNotNull(getAllResult);
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_MapSkillsToAssociate_Exception()
        {
            var _mockDashboardProvider = new Mock<IDashboardProvider>();
            _mockDashboardProvider
                .Setup(s => s.DeleteAssociateSkillMapping(It.IsAny<int>()))
                .Throws(new Exception("Mock Unit Test Exception"));

            this._associateController = new AssociatesController(null, null, null, null, null, _mockDashboardProvider.Object);

            //// Mao Skills to Associate
            var mapResponse = this._associateController.MapSkillsToAssociate(-4, new List<AssociateSkill>
            {
                new AssociateSkill { AssociateId = -4, SkillId = 1, Rating = 10},
                new AssociateSkill { AssociateId = -4, SkillId = 1, Rating = 20}
            });
            Assert.IsNotNull(mapResponse);

            var mapResult = mapResponse as InternalServerErrorResult;
            Assert.IsNotNull(mapResult);            
        }

        [Test]
        [Category("AssociatesControllerMockTest")]
        public void TestMethod_GetSkillReport_Exception()
        {
            var _mockDashboardProvider = new Mock<IDashboardProvider>();
            _mockDashboardProvider
                .Setup(s => s.GetAssociateSkillReport())
                .Throws(new Exception("Mock Unit Test Exception"));

            this._associateController = new AssociatesController(null, null, null, null, null, _mockDashboardProvider.Object);

            //// Get Skill Report
            var getAllResponse = this._associateController.GetSkillReport();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as InternalServerErrorResult;
            Assert.IsNotNull(getAllResult);            
        }
    }
}
