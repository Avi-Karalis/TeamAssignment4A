using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TeamAssignment4A.Models.IdentityUsers;
using Microsoft.AspNetCore.Identity;

namespace TeamAssignment4A.Data {
    public class WebAppDbContext : IdentityDbContext<AppUser> {
        //public WebAppDbContext(DbContextOptions options) : base(options) {

        //}
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options) {

        }
        //public virtual DbSet<IdentityUser> User { get; set; }
        public virtual DbSet<AppUser> User { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Stem> Stems { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        //public virtual DbSet<CandidateCertificate> CandidateCertificates { get; set; }
        //public virtual DbSet<ExamStem> ExamStems { get; set; }
        //public virtual DbSet<ExamTopic> ExamTopics { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder) {
        //    modelBuilder.Entity<ExamStem>()
        //    .HasKey(es => new { es.Exams, es.Stems });
        //    modelBuilder.Entity<ExamStem>()
        //                .HasOne(es => es.Exams)
        //                .WithMany(e => e.ExamStems)
        //                .HasForeignKey(es => es.Exams);
        //    modelBuilder.Entity<ExamStem>()
        //                .HasOne(es => es.Stems)
        //                .WithMany(s => s.ExamStems)
        //                .HasForeignKey(es => es.Stems);
            

        //}
    }
    // old db context with out Identity
    ////public class WebAppDbContext : DbContext {
    //    public WebAppDbContext(DbContextOptions options) : base(options) {

    //    }
    //    public virtual DbSet<Candidate> Candidates { get; set; }
    //    public virtual DbSet<Certificate> Certificates { get; set; }
    //    public virtual DbSet<Exam> Exams { get; set; }
    //    public virtual DbSet<Stem> Stems { get; set; }
    //    public virtual DbSet<Topic> Topics { get; set; }
    //    //public virtual DbSet<CandidateCertificate> CandidateCertificates { get; set; }
    //    //public virtual DbSet<ExamStem> ExamStems { get; set; }
    //    //public virtual DbSet<ExamTopic> ExamTopics { get; set; }

    //    //protected override void OnModelCreating(ModelBuilder modelBuilder) {
    //    //    modelBuilder.Entity<ExamStem>()
    //    //    .HasKey(es => new { es.Exams, es.Stems });
    //    //    modelBuilder.Entity<ExamStem>()
    //    //                .HasOne(es => es.Exams)
    //    //                .WithMany(e => e.ExamStems)
    //    //                .HasForeignKey(es => es.Exams);
    //    //    modelBuilder.Entity<ExamStem>()
    //    //                .HasOne(es => es.Stems)
    //    //                .WithMany(s => s.ExamStems)
    //    //                .HasForeignKey(es => es.Stems);


    //    //}
    }

