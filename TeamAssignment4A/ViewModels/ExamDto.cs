using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Dtos
{
    public class ExamDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Assessment Test Code")]
        public string AssessmentTestCode { get; set; }


        [Display(Name = "Examination Date")]
        [Column(TypeName = "Date")]
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

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public string TitleOfCertificate { get; set; }
        public Certificate Certificate { get; set; }
    }
}
