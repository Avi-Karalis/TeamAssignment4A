using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models.JointTables {
    public class ExamStem {
        public int Id { get; set; } 
        public char SubmittedAnswer { get; set; }
        public int Score { get; set; }

        // Navigation Properties
        public virtual Exam Exams { get; set; }
        public virtual Stem Stems { get; set; }

    }
}
