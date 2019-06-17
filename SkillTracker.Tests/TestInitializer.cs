using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Linq;
using NUnit.Framework;
using SkillTracker.Data.EFCore;
using SkillTracker.Data.Models;
using SkillTracker.Service.Controllers;

namespace SkillTracker.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        private SkillsController _skillController;
        private AssociatesController _associatesController;

        public TestInitializer()
        {
            this._skillController = new SkillsController();
            this._associatesController = new AssociatesController();
        }

        ////[OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            //// Initialize EF Database
            System.Data.Entity.Database.SetInitializer(new SkillTrackerDbInitializer());
        }

        ////[OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // Clear SkillsController Test Data If Any
            ((OkNegotiatedContentResult<List<Skill>>)this._skillController.GetSkills())
                .Content
                .Where(w => w.Name.StartsWith("UnitTest_", StringComparison.OrdinalIgnoreCase))
                .ToList()
                .ForEach(itemToDelete =>
                {
                    this._skillController.DeleteSkill(itemToDelete.Id);
                });

            // Clear AssociatesController Test Data If Any
            ((OkNegotiatedContentResult<List<Associate>>)this._associatesController.GetAssociates())
                .Content
                .Where(w => w.Name.StartsWith("UnitTest_", StringComparison.OrdinalIgnoreCase))
                .ToList()
                .ForEach(itemToDelete =>
                {
                    this._associatesController.DeleteAssociate(itemToDelete.Id);
                });
        }
    }
}
