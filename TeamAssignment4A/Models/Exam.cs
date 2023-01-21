using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Xml.Linq;
using TeamAssignment4A.Models.JointTables;


namespace TeamAssignment4A.Models
{
    public class Exam {
        public int Id { get; set; }
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
        [Display(Name = "Percentage Score")]
        public string PercentageScore { get; set; }

        [Required]
        [Display(Name = "Assessment Result Label")]
        public string AssessmentResultLabel { get; set; }


        // Navigation Properties

        public virtual Certificate Certificate { get; set; }
        public virtual ICollection<CandidateExam>? CandidateExams { get; set; }        
        public virtual ICollection<ExamStem>? ExamStems { get; set; }
    }
   
}
