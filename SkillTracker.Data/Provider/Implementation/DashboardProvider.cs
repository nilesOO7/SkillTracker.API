using System;
using System.Collections.Generic;
using System.Linq;
using SkillTracker.Data.Models;
using SkillTracker.Data.Provider.Contract;
using SkillTracker.Data.Repository.Contract;
using SkillTracker.Data.Repository.Implementation;

namespace AssociateTracker.Data.Provider.Implementation
{
    public class DashboardProvider : IDashboardProvider
    {
        private ISkillMasterRepository _skillMasterRepo;
        private IAssociateRepository _associateRepo;
        private IAssociateSkillMapRepository _associateSkillMapRepo;

        [ExcludeFromCoverage]
        public DashboardProvider(
            ISkillMasterRepository skillMasterRepo,
            IAssociateRepository associateRepo,
            IAssociateSkillMapRepository associateSkillMapRepo)
        {
            if (skillMasterRepo != null)
            {
                this._skillMasterRepo = skillMasterRepo;
            }
            else
            {
                this._skillMasterRepo = new SkillMasterRepository();
            }

            if (associateRepo != null)
            {
                this._associateRepo = associateRepo;
            }
            else
            {
                this._associateRepo = new AssociateRepository();
            }

            if (associateSkillMapRepo != null)
            {
                this._associateSkillMapRepo = associateSkillMapRepo;
            }
            else
            {
                this._associateSkillMapRepo = new AssociateSkillMapRepository();
            }
        }

        public List<Associate> GetAssociateSkillDetails()
        {
            var skillDetails =
                this._associateSkillMapRepo
                    .GetAssociateSkillMappings()
                    .Join(
                        this._skillMasterRepo.GetSkills(),
                        l => l.SkillId,
                        r => r.Id,
                        (l, r) => new
                        {
                            AssociateId = l.AssociateId,
                            SkillId = r.Id,
                            SkillName = r.Name
                        }
                    );

            var assocSkillDetails =
                this._associateRepo
                    .GetAssociates()
                    .GroupJoin(
                        skillDetails,
                        left => left.Id,
                        right => right.AssociateId,
                        (x, y) => new { Left = x, Right = y }
                    )
                    .SelectMany(
                        x => x.Right.DefaultIfEmpty(),
                        (x, y) => new { Left = x.Left, Right = y }
                    )
                    .GroupBy(g => g.Left.Id)
                    .Select(s => new Associate
                    {
                        Id = s.Key,
                        Picture = s.FirstOrDefault().Left.Picture,
                        Status = s.FirstOrDefault().Left.Status,
                        Name = s.FirstOrDefault().Left.Name,
                        Email = s.FirstOrDefault().Left.Email,
                        Mobile = s.FirstOrDefault().Left.Mobile,
                        StrongSkills
                            = s.FirstOrDefault().Right == null
                                ? string.Empty
                                : string.Join(
                                    ", ",
                                    s.OrderBy(o => o.Right.SkillId)
                                     .Select(x => x.Right.SkillName))
                    });

            return assocSkillDetails.ToList();
        }

        public List<SkillMapForAssociate> GetAssociateSkillMap(int associateId)
        {
            var skillMapForAssoc =
                this._skillMasterRepo
                    .GetSkills()
                    .GroupJoin(
                        this._associateSkillMapRepo
                            .GetAssociateSkillMappings()
                            .Where(w => w.AssociateId == associateId),
                        left => left.Id,
                        right => right.SkillId,
                        (x, y) => new { Left = x, Right = y }
                    )
                    .SelectMany(
                        x => x.Right.DefaultIfEmpty(),
                        (x, y) => new { Left = x.Left, Right = y }
                    )
                    .Select(s => new SkillMapForAssociate
                    {
                        SkillId = s.Left.Id,
                        Name = s.Left.Name,
                        IsTechnical = s.Left.IsTechnical,
                        IsSelected = s.Right != null,
                        Rating = s.Right != null ? s.Right.Rating : 0
                    });

            return skillMapForAssoc.ToList();
        }

        public int AddAssociateSkillMapping(List<AssociateSkill> inputSkillMap)
        {
            return this._associateSkillMapRepo.AddAssociateSkillMapping(inputSkillMap);
        }

        public int DeleteAssociateSkillMapping(int associateId)
        {
            return this._associateSkillMapRepo.DeleteAssociateSkillMapping(associateId);
        }

        public Report GetAssociateSkillReport()
        {
            Report skillReport = new Report();

            var skillDetails =
                this._associateSkillMapRepo
                    .GetAssociateSkillMappings()
                    .Join(
                        this._skillMasterRepo.GetSkills(),
                        l => l.SkillId,
                        r => r.Id,
                        (l, r) => new
                        {
                            AssociateId = l.AssociateId,
                            SkillId = r.Id,
                            SkillName = r.Name
                        }
                    );

            var skillCount = skillDetails
                        .GroupBy(g => g.SkillId)
                        .Select(s => new
                        {
                            SkillId = s.Key,
                            SkillName = s.FirstOrDefault().SkillName,
                            AssociateCount = s.Count()
                        });

            var total = skillCount.Sum(s => s.AssociateCount);

            var skillSpread = skillCount
                                .Select(s => new AssociateSkill
                                {
                                    SkillId = s.SkillId,
                                    SkillName = s.SkillName,
                                    Percentage = Math.Round(((decimal)s.AssociateCount / total) * 100, 2)
                                });

            skillReport.SkillSpread = skillSpread.ToList();

            skillReport.RegisteredCandidateCount = this._associateRepo.GetAssociates().Count;

            var femaleAssocCount = this._associateRepo.GetAssociates().Where(w => w.Gender == "F").Count();
            var maleAssocCount = this._associateRepo.GetAssociates().Where(w => w.Gender == "M").Count();
            var fresherCount = this._associateRepo.GetAssociates().Where(w => w.IsFresher).Count();

            var l1Candidate = this._associateRepo.GetAssociates().Where(w => w.Level == 1).Count();
            var l2Candidate = this._associateRepo.GetAssociates().Where(w => w.Level == 2).Count();
            var l3Candidate = this._associateRepo.GetAssociates().Where(w => w.Level == 3).Count();

            if (skillReport.RegisteredCandidateCount > 0)
            {
                if (femaleAssocCount > 0)
                {
                    skillReport.FemaleCandidatePercentage = Math.Round(((decimal)femaleAssocCount / skillReport.RegisteredCandidateCount) * 100, 2);
                }

                if (maleAssocCount > 0)
                {
                    skillReport.MaleCandidatePercentage = Math.Round(((decimal)maleAssocCount / skillReport.RegisteredCandidateCount) * 100, 2);
                }

                if (fresherCount > 0)
                {
                    skillReport.FresherCandidatePercentage = Math.Round(((decimal)fresherCount / skillReport.RegisteredCandidateCount) * 100, 2);
                }

                if (l1Candidate > 0)
                {
                    skillReport.Level1CandidatePercentage = Math.Round(((decimal)l1Candidate / skillReport.RegisteredCandidateCount) * 100, 2);
                }

                if (l2Candidate > 0)
                {
                    skillReport.Level2CandidatePercentage = Math.Round(((decimal)l2Candidate / skillReport.RegisteredCandidateCount) * 100, 2);
                }

                if (l3Candidate > 0)
                {
                    skillReport.Level3CandidatePercentage = Math.Round(((decimal)l3Candidate / skillReport.RegisteredCandidateCount) * 100, 2);
                }
            }

            var ratedAssoc = skillDetails.Select(s => s.AssociateId).Distinct();

            skillReport.RatedCandidateCount = ratedAssoc.Count();

            var ratedFemaleAssoc = this._associateRepo.GetAssociates().Where(w => w.Gender == "F" && ratedAssoc.Contains(w.Id)).Count();
            var ratedMaleAssoc = this._associateRepo.GetAssociates().Where(w => w.Gender == "M" && ratedAssoc.Contains(w.Id)).Count();

            if (skillReport.RatedCandidateCount > 0)
            {
                if (ratedFemaleAssoc > 0)
                {
                    skillReport.FemaleRatedCandidatePercentage = Math.Round(((decimal)ratedFemaleAssoc / skillReport.RatedCandidateCount) * 100, 2);
                }

                if (ratedMaleAssoc > 0)
                {
                    skillReport.MaleRatedCandidatePercentage = Math.Round(((decimal)ratedMaleAssoc / skillReport.RatedCandidateCount) * 100, 2);
                }
            }

            return skillReport;
        }
    }
}
