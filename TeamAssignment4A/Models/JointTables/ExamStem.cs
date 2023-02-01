using System.ComponentModel.DataAnnotations;

namespace TeamAssignment4A.Models.JointTables 
{
    public class ExamStem 
    {
        public int Id { get; set; }

        [Display(Name = "Answer")]
        public char SubmittedAnswer { get; set; }
        public int Score { get; set; }

        // Navigation Properties
        public virtual Exam Exam { get; set; }
        public virtual Stem Stem { get; set; }

    }
}
