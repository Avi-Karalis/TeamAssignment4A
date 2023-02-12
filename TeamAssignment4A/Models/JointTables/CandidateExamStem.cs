using System.ComponentModel.DataAnnotations;


namespace TeamAssignment4A.Models.JointTables
{
    public class CandidateExamStem
    {
        [Key]
        [Required]
        [Display(Name = "Candidate Exam Stem Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Answer")]
        public char SubmittedAnswer { get; set; }
        public int Score { get; set; }

        
        // Navigation Properties
        public virtual ExamStem ExamStem { get; set; }
        public virtual CandidateExam CandidateExam { get; set; }
    }
}
