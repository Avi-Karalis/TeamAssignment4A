﻿using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TeamAssignment4A.Models.IdentityUsers;
using Microsoft.AspNetCore.Identity;
using TeamAssignment4A.Dtos;

namespace TeamAssignment4A.Data
{
    public class WebAppDbContext : IdentityDbContext<AppUser>
    {
        
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
        {

        }
        
        public virtual DbSet<AppUser> User { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Stem> Stems { get; set; }
        public virtual DbSet<ExamTopic> ExamTopics { get; set; }
        public virtual DbSet<ExamStem> ExamStems { get; set; }
        public virtual DbSet<Score> Scores { get; set; }
        public virtual DbSet<CandidateExam> CandidateCertificates { get; set; }
        public DbSet<TopicDto> TopicDto { get; set; }
    }
}

