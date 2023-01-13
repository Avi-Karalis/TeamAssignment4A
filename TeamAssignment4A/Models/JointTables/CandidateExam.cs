using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models.JointTables {
    public class CandidateExam {

        public int CandidateExamId { get; set; }

        //[ForeignKey("Candidate"), Column(Order = 0)]
        //public int CandidateId { get; set; }

        //[ForeignKey("ExamId"), Column(Order = 1)]
        //public int ExamId { get; set; }

        //[ForeignKey("Score"), Column(Order = 2)]
        //public int ScoresId { get; set; }

        [Required]
        [Display(Name = "Assessment Test Code")]
        public int AssessmentTestCode { get; set; }

        [Required]
        [Display(Name = "Examination Date")]
        [Column(TypeName = "Date")]
        public DateTime ExaminationDate { get; set; }

        [Required]
        [Display(Name = "Score Report Date")]
        [Column(TypeName = "Date")]
        public DateTime ScoreReportDate { get; set; }

        [Required]
        [Display(Name = "Candidate Score")]
        public int CandidateScore { get; set; }

        [Required]
        [Display(Name = "Assessment Result Label")]
        public string AssessmentResultLabel { get; set; }

        [Required]
        [Display(Name = "Percentage Score")]
        public string PercentageScore { get; set; }

        public virtual Candidate Candidates { get; set; }
        public virtual Exam Exams { get; set; }
        public virtual Score Scores { get; set; }
    }
}
