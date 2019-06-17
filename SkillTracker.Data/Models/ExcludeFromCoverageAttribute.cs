using System;

namespace SkillTracker.Data.Models
{
    [ExcludeFromCoverage]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor)]
    public class ExcludeFromCoverageAttribute : Attribute
    {
        //// To be used to exclude OpenCover test coverage
    }
}
