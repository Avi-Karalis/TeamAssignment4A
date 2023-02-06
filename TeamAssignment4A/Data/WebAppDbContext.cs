using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;

using TeamAssignment4A.Dtos;


namespace TeamAssignment4A.Data
{
    public class WebAppDbContext : ApiAuthorizationDbContext<IdentityUser>
    {
        
        public WebAppDbContext(DbContextOptions options ,IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) {

        }

        public virtual DbSet<IdentityUser> User { get; set; } = default!;
        public virtual DbSet<Candidate> Candidates { get; set; } = default!;
        public virtual DbSet<Certificate> Certificates { get; set; } = default!;
        public virtual DbSet<Exam> Exams { get; set; } = default!;
        public virtual DbSet<Topic> Topics { get; set; } = default!;
        public virtual DbSet<Stem> Stems { get; set; } = default!;        
        public virtual DbSet<ExamStem> ExamStems { get; set; } = default!;        
        public virtual DbSet<CandidateExamStem> CandidateExamStems { get; set; } = default!;
        public virtual DbSet<CandidateExam> CandidateExams { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            modelBuilder.Entity<Certificate>().HasIndex(c => c.TitleOfCertificate).IsUnique();
            modelBuilder.Entity<Topic>().HasIndex(c => c.Description).IsUnique();            
            modelBuilder.Entity<CandidateExam>().HasIndex(c => c.AssessmentTestCode).IsUnique();            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
    }
}

