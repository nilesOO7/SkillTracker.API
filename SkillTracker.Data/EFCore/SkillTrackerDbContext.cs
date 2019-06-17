using SkillTracker.Data.Models;
using System.Data.Entity;

namespace SkillTracker.Data.EFCore
{
    public class SkillTrackerDbContext : DbContext
    {
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Associate> Associates { get; set; }
        public DbSet<AssociateSkill> AssociateSkills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            tbl_Skills
            */
            modelBuilder.Entity<Skill>().ToTable("tbl_Skills");
            modelBuilder.Entity<Skill>().HasKey<int>(s => s.Id);
            modelBuilder.Entity<Skill>()
                .Property(s => s.Id)
                .HasColumnName("Skill_ID")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Skill>()
                .Property(s => s.Name)
                .HasColumnName("Skill_Name")
                .HasMaxLength(128)
                .IsRequired();

            /*
            tbl_Associates
            */
            modelBuilder.Entity<Associate>().ToTable("tbl_Associates");
            modelBuilder.Entity<Associate>().Ignore(i => i.StrongSkills);
            modelBuilder.Entity<Associate>().Ignore(i => i.PictureBase64String);
            modelBuilder.Entity<Associate>().HasKey<int>(s => s.Id);
            modelBuilder.Entity<Associate>()
                .Property(s => s.Id)
                .HasColumnName("Associate_ID")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<Associate>().Property(s => s.Picture).HasColumnName("Pic").HasMaxLength(256);
            modelBuilder.Entity<Associate>().Property(s => s.Status).HasMaxLength(8);
            modelBuilder.Entity<Associate>().Property(s => s.Gender).HasMaxLength(1);
            modelBuilder.Entity<Associate>().Property(s => s.Name).HasMaxLength(256);
            modelBuilder.Entity<Associate>().Property(s => s.Email).HasMaxLength(256);
            modelBuilder.Entity<Associate>().Property(s => s.Mobile).HasMaxLength(16);
            modelBuilder.Entity<Associate>().Property(s => s.Remark).HasMaxLength(256);
            modelBuilder.Entity<Associate>().Property(s => s.Strength).HasMaxLength(128);
            modelBuilder.Entity<Associate>().Property(s => s.Weakness).HasMaxLength(128);
            modelBuilder.Entity<Associate>().Property(s => s.OtherSkill).HasMaxLength(256);

            /*
            tbl_AssociateSkills
            */
            modelBuilder.Entity<AssociateSkill>().ToTable("tbl_AssociatesSkills");
            modelBuilder.Entity<AssociateSkill>().Ignore(i => i.Percentage);
            modelBuilder.Entity<AssociateSkill>().Ignore(i => i.SkillName);
            modelBuilder.Entity<AssociateSkill>().HasKey(s => new { s.AssociateId, s.SkillId });
            modelBuilder.Entity<AssociateSkill>()
                .HasRequired<Associate>(ask => ask.Associate)
                .WithMany(a => a.AssociateSkills)
                .HasForeignKey<int>(ask => ask.AssociateId);
            modelBuilder.Entity<AssociateSkill>()
                .HasRequired<Skill>(ask => ask.Skill)
                .WithMany(s => s.AssociateSkills)
                .HasForeignKey<int>(ask => ask.SkillId);
            modelBuilder.Entity<AssociateSkill>().Property(s => s.AssociateId).HasColumnName("Associate_ID");
            modelBuilder.Entity<AssociateSkill>().Property(s => s.SkillId).HasColumnName("Skill_ID");
            modelBuilder.Entity<AssociateSkill>().Property(s => s.Rating).HasColumnName("Skill_Rating");

            //// base.OnModelCreating(modelBuilder);
        }
    }
}
