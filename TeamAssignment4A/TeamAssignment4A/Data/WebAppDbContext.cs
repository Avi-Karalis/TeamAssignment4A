using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;
using Microsoft.EntityFrameworkCore;

namespace TeamAssignment4A.Data {
    public class WebAppDbContext : DbContext {
        public WebAppDbContext(DbContextOptions options) : base(options) {
            
        }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Stem> Stems { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
    }
}
