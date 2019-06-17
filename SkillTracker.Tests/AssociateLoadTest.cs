using System;
using System.Collections.Generic;
using SkillTracker.Service.Controllers;
using SkillTracker.Data.Models;
using NBench;

namespace SkillTracker.Tests
{
    public class AssociateLoadTest
    {
        private AssociatesController _assocController;

        private Counter _assocAddCounter;
        private Counter _assocFetchCounter;
        private Counter _reportFetchCounter;

        private int refId = -1000;
        List<int> _associateIdList;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _assocController = new AssociatesController();
            _associateIdList = new List<int>();
        }

        [PerfBenchmark(
            Description = "Load Test For New Associate Addition",
            NumberOfIterations = 500,
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement,
            RunTimeMilliseconds = 1200,
            SkipWarmups = true)]
        [CounterThroughputAssertion("AssociateAddCounter", MustBe.GreaterThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        [CounterMeasurement("AssociateAddCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Benchmark_AddAssociate(BenchmarkContext context)
        {
            _assocAddCounter = context.GetCounter("AssociateAddCounter");

            int assocId = refId--;
            Associate associate = new Associate { Id = assocId, Name = "LoadTest_PostAssociate_Success_" + DateTime.Now.Ticks, Email = "test@ymail.com", Mobile = "+1 11111111", Picture = "", Status = "Green", Level = 1, Remark = "Top performer", Strength = "Coding", Weakness = "Team Building", Gender = "M", IsFresher = false, OtherSkill = "CSR Related Works" };
            var postResponse = this._assocController.PostAssociate(associate);

            _assocAddCounter.Increment();
            _associateIdList.Add(assocId);
        }

        [PerfBenchmark(
            Description = "Load Test For Retrieval of Associate Details",
            NumberOfIterations = 500,
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement,
            RunTimeMilliseconds = 1200,
            SkipWarmups = true)]
        [CounterThroughputAssertion("AssociateFetchCounter", MustBe.GreaterThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        [CounterMeasurement("AssociateFetchCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Benchmark_FetchAssociate(BenchmarkContext context)
        {
            _assocFetchCounter = context.GetCounter("AssociateFetchCounter");
            var postResponse = _assocController.GetAssociates();
            _assocFetchCounter.Increment();
        }

        [PerfBenchmark(
            Description = "Load Test For Retrieval of Associate Skill Report",
            NumberOfIterations = 500,
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement,
            RunTimeMilliseconds = 1200,
            SkipWarmups = true)]
        [CounterThroughputAssertion("ReportFetchCounter", MustBe.GreaterThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        [CounterMeasurement("ReportFetchCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Benchmark_FetchAssociateSkillReport(BenchmarkContext context)
        {
            _reportFetchCounter = context.GetCounter("ReportFetchCounter");
            var postResponse = _assocController.GetSkillReport();
            _reportFetchCounter.Increment();
        }

        [PerfCleanup]
        public void Cleanup()
        {
            foreach (int assocId in _associateIdList)
            {
                var result = _assocController.DeleteAssociate(assocId);
            }
        }
    }
}
