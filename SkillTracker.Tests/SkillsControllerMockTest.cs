using System;
using System.Web.Http.Results;
using NUnit.Framework;
using SkillTracker.Data.Models;
using SkillTracker.Service.Controllers;
using SkillTracker.Data.Provider.Contract;
using Moq;

namespace SkillTracker.Tests
{
    [TestFixture]
    public class SkillsControllerMockTest
    {
        private SkillsController _skillController;

        public SkillsControllerMockTest()
        {
            this._skillController = new SkillsController();
        }

        [Test]
        [Category("SkillsControllerMockTest")]
        public void TestMethod_GetAllSkills_Exception()
        {
            var _mockSkillProvider = new Mock<ISkillMasterProvider>();
            _mockSkillProvider
                .Setup(s => s.GetSkills())
                .Throws(new Exception("Mock Unit Test Exception"));

            this._skillController = new SkillsController(null, _mockSkillProvider.Object);

            var getAllResponse = this._skillController.GetSkills();
            Assert.IsNotNull(getAllResponse);

            var getAllResult = getAllResponse as InternalServerErrorResult;
            Assert.IsNotNull(getAllResult);
        }

        [Test]
        [Category("SkillsControllerMockTest")]
        public void TestMethod_GetSkill_Exception()
        {
            var _mockSkillProvider = new Mock<ISkillMasterProvider>();
            _mockSkillProvider
                .Setup(s => s.GetSkill(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Throws(new Exception("Mock Unit Test Exception"));

            this._skillController = new SkillsController(null, _mockSkillProvider.Object);
            var getResponse = this._skillController.GetSkill(1);
            Assert.IsNotNull(getResponse);

            var getResult = getResponse as InternalServerErrorResult;
            Assert.IsNotNull(getResult);
        }

        [Test]
        [Category("SkillsControllerMockTest")]
        public void TestMethod_PostSkill_Exception()
        {
            var _mockSkillProvider = new Mock<ISkillMasterProvider>();
            _mockSkillProvider
                .Setup(s => s.AddSkill(It.IsAny<Skill>()))
                .Throws(new Exception("Mock Unit Test Exception"));

            this._skillController = new SkillsController(null, _mockSkillProvider.Object);

            Skill skill = new Skill { Id = -1, Name = "UnitTest_PostSkill_Success", IsTechnical = false };
            var postResponse = this._skillController.PostSkill(skill);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as InternalServerErrorResult;
            Assert.IsNotNull(postResult);
        }

        [Test]
        [Category("SkillsControllerMockTest")]
        public void TestMethod_PostSkill_Failure()
        {
            var _mockSkillProvider = new Mock<ISkillMasterProvider>();
            _mockSkillProvider
                .Setup(s => s.AddSkill(It.IsAny<Skill>()))
                .Returns(() => { return 0; });

            this._skillController = new SkillsController(null, _mockSkillProvider.Object);

            Skill skill = new Skill { Id = -1, Name = "UnitTest_PostSkill_Success", IsTechnical = false };
            var postResponse = this._skillController.PostSkill(skill);
            Assert.IsNotNull(postResponse);

            var postResult = postResponse as InternalServerErrorResult;
            Assert.IsNotNull(postResult);
        }

        [Test]
        [Category("SkillsControllerMockTest")]
        public void TestMethod_PutSkill_NotFound()
        {
            var _mockSkillProvider = new Mock<ISkillMasterProvider>();
            _mockSkillProvider
                .Setup(s => s.GetSkill(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return null; });

            this._skillController = new SkillsController(null, _mockSkillProvider.Object);

            var skillToUpdate = new Skill { Id = -1, Name = "UnitTest_PutSkill_Updated_NotFound" };
            var putResponse = this._skillController.PutSkill(skillToUpdate);
            Assert.IsNotNull(putResponse);

            var putResult = putResponse as NotFoundResult;
            Assert.IsNotNull(putResult);
        }

        [Test]
        [Category("SkillsControllerMockTest")]
        public void TestMethod_PutSkill_Exception()
        {
            var _mockSkillProvider = new Mock<ISkillMasterProvider>();
            _mockSkillProvider
                .Setup(s => s.GetSkill(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return new Skill(); });
            _mockSkillProvider
                .Setup(s => s.EditSkill(It.IsAny<Skill>()))
                .Throws(new Exception("Mock Unit Test Exception"));

            this._skillController = new SkillsController(null, _mockSkillProvider.Object);

            var skillToUpdate = new Skill { Id = -1, Name = "UnitTest_PutSkill_Updated_NotFound" };
            var putResponse = this._skillController.PutSkill(skillToUpdate);
            Assert.IsNotNull(putResponse);

            var putResult = putResponse as InternalServerErrorResult;
            Assert.IsNotNull(putResult);
        }

        [Test]
        [Category("SkillsControllerMockTest")]
        public void TestMethod_PutSkill_Failure()
        {
            var _mockSkillProvider = new Mock<ISkillMasterProvider>();
            _mockSkillProvider
                .Setup(s => s.GetSkill(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return new Skill(); });
            _mockSkillProvider
                .Setup(s => s.EditSkill(It.IsAny<Skill>()))
                .Returns(() => { return 0; });

            this._skillController = new SkillsController(null, _mockSkillProvider.Object);

            var skillToUpdate = new Skill { Id = -1, Name = "UnitTest_PutSkill_Updated_NotFound" };
            var putResponse = this._skillController.PutSkill(skillToUpdate);
            Assert.IsNotNull(putResponse);

            var putResult = putResponse as InternalServerErrorResult;
            Assert.IsNotNull(putResult);
        }

        [Test]
        [Category("SkillsControllerMockTest")]
        public void TestMethod_DeleteSkill_NotFound()
        {
            var _mockSkillProvider = new Mock<ISkillMasterProvider>();
            _mockSkillProvider
                .Setup(s => s.GetSkill(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return null; });

            this._skillController = new SkillsController(null, _mockSkillProvider.Object);

            var delResponse = this._skillController.DeleteSkill(-1);
            Assert.IsNotNull(delResponse);

            var delResult = delResponse as NotFoundResult;
            Assert.IsNotNull(delResult);
        }

        [Test]
        [Category("SkillsControllerMockTest")]
        public void TestMethod_DeleteSkill_Exception()
        {
            var _mockSkillProvider = new Mock<ISkillMasterProvider>();
            _mockSkillProvider
                .Setup(s => s.GetSkill(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return new Skill(); });
            _mockSkillProvider
                .Setup(s => s.DeleteSkill(It.IsAny<Skill>()))
                .Throws(new Exception("Mock Unit Test Exception"));

            this._skillController = new SkillsController(null, _mockSkillProvider.Object);

            var delResponse = this._skillController.DeleteSkill(-1);
            Assert.IsNotNull(delResponse);

            var delResult = delResponse as InternalServerErrorResult;
            Assert.IsNotNull(delResult);
        }

        [Test]
        [Category("SkillsControllerMockTest")]
        public void TestMethod_DeleteSkill_Failure()
        {
            var _mockSkillProvider = new Mock<ISkillMasterProvider>();
            _mockSkillProvider
                .Setup(s => s.GetSkill(It.IsAny<int>(), It.IsAny<Boolean>()))
                .Returns(() => { return new Skill(); });
            _mockSkillProvider
                .Setup(s => s.DeleteSkill(It.IsAny<Skill>()))
                .Returns(() => { return 0; });

            this._skillController = new SkillsController(null, _mockSkillProvider.Object);

            var delResponse = this._skillController.DeleteSkill(-1);
            Assert.IsNotNull(delResponse);

            var delResult = delResponse as InternalServerErrorResult;
            Assert.IsNotNull(delResult);
        }
    }
}
