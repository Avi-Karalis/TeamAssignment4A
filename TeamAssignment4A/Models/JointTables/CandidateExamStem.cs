using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TeamAssignment4A.Models.JointTables
{
    public class CandidateExamStem
    {
        [Display(Name = "Candidate Exam Stem Id")]
        public int Id { get; set; }

        [Display(Name = "Answer")]
        public char SubmittedAnswer { get; set; }
        public int Score { get; set; }

        
        // Navigation Properties
        public virtual Candidate Candidate { get; set; }
        public virtual ExamStem ExamStem { get; set; }
        public virtual CandidateExam CandidateExam { get; set; }
    }
}
