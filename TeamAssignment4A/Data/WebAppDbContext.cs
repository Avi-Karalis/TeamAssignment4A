using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TeamAssignment4A.Models.IdentityUsers;
using Microsoft.AspNetCore.Identity;


namespace TeamAssignment4A.Data
{
    public class WebAppDbContext : IdentityDbContext<Candidate>
    {
        
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<AppUser> User { get; set; } = default!;
        public virtual DbSet<Candidate> Candidates { get; set; } = default!;
        public virtual DbSet<Certificate> Certificates { get; set; } = default!;
        public virtual DbSet<Exam> Exams { get; set; } = default!;
        public virtual DbSet<Topic> Topics { get; set; } = default!;
        public virtual DbSet<Stem> Stems { get; set; } = default!;        
        public virtual DbSet<ExamStem> ExamStems { get; set; } = default!;        
        public virtual DbSet<CandidateExam> CandidateExams { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            modelBuilder.Entity<Certificate>().HasIndex(c => c.TitleOfCertificate).IsUnique();
            modelBuilder.Entity<Topic>().HasIndex(c => c.Description).IsUnique();
            modelBuilder.Entity<Exam>().HasIndex(c => c.AssessmentTestCode).IsUnique();
        }
    }
}

