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
        
        public string AssessmentTestCode { get; set; }
        [Required]
        public DateTime ExaminationDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? ScoreReportDate { get; set; }
        
        public int? CandidateScore { get; set; }

        public string? PercentageScore { get; set; }
        
        public string? AssessmentResultLabel { get; set; }

        [Required]
        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }

        [Required]
        public string TitleOfCertificate { get; set; }

        public Certificate Certificate { get; set; }
    }
}
