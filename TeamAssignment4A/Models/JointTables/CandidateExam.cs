using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models.JointTables {
    public class CandidateExam 
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Assessment Test Code")]
        public string AssessmentTestCode { get; set; }


        [Display(Name = "Examination Date")]
        public DateTime? ExaminationDate { get; set; }


        [Display(Name = "Score Report Date")]
        [Column(TypeName = "Date")]
        public DateTime? ScoreReportDate { get; set; }


        [Display(Name = "Candidate Score")]
        public int? CandidateScore { get; set; }


        [Display(Name = "Percentage Score")]
        public string? PercentageScore { get; set; }


        [Display(Name = "Assessment Result Label")]
        public string? AssessmentResultLabel { get; set; }


        // Navigation Properties
        public virtual Candidate Candidate { get; set; }
        public virtual Exam Exam { get; set; }
        
    }
}
