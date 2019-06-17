using System;
using System.Collections.Generic;
using SkillTracker.Service.Controllers;
using SkillTracker.Data.Models;
using System.Web.Http.Results;
using NBench;

namespace SkillTracker.Tests
{
    public class SkillLoadTest
    {
        private SkillsController _skillController;

        private Counter _skillAddCounter;
        private Counter _skillFetchCounter;

        List<int> _skillIdList;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _skillController = new SkillsController();

            //_skillAddCounter = context.GetCounter("SkillAddCounter");
            //_skillFetchCounter = context.GetCounter("SkillFetchCounter");

            _skillIdList = new List<int>();
        }

        [PerfBenchmark(
            Description = "Load Test For New Skill Addition",
            NumberOfIterations = 500,
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement,
            RunTimeMilliseconds = 1200,            
            SkipWarmups = true)]
        [CounterThroughputAssertion("SkillAddCounter", MustBe.GreaterThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        [CounterMeasurement("SkillAddCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Benchmark_AddSkill(BenchmarkContext context)
        {
            _skillAddCounter = context.GetCounter("SkillAddCounter");

            Skill skill = new Skill { Id = -1, Name = "LoadTest_PostSkill_Success_" + DateTime.Now.Ticks, IsTechnical = false };
            var postResponse = _skillController.PostSkill(skill);
            var skillId = (postResponse as OkNegotiatedContentResult<int>).Content;

            _skillAddCounter.Increment();
            _skillIdList.Add(skillId);
        }

        [PerfBenchmark(
            Description = "Load Test For Retrieval of Skills",
            NumberOfIterations = 500,
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement,
            RunTimeMilliseconds = 1200,
            SkipWarmups = true)]
        [CounterThroughputAssertion("SkillFetchCounter", MustBe.GreaterThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        [CounterMeasurement("SkillFetchCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Benchmark_FetchSkill(BenchmarkContext context)
        {
            _skillFetchCounter = context.GetCounter("SkillFetchCounter");
            var postResponse = _skillController.GetSkills();            
            _skillFetchCounter.Increment();            
        }

        [PerfCleanup]
        public void Cleanup()
        {
            foreach (int skillId in _skillIdList)
            {
                var result = _skillController.DeleteSkill(skillId);
            }
        }
    }
}
