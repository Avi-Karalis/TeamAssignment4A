using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models {
    public class Certificate {
        //Basic Info for Certificate
        [Key]
        [Required]
        [Display(Name = "Certificate Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Title Of Certificate")]
        public string TitleOfCertificate { get; set; }

        [Required]
        [Display(Name = "Candidate Number")]
        public int CandidateNumber { get; set; }

        [Required]
        [Display(Name = "Assessment Test Code")]
        public int AssessmentTestCode { get; set; }

        [Required]
        [Display(Name = "Examination Date")]
        public DateTime ExaminationDate { get; set; }

        [Required]
        [Display(Name = "Score Report Date")]
        public DateTime ScoreReportDate { get; set; }

        [Required]
        [Display(Name = "Candidate Score")]
        public int CandidateScore { get; set; }

        [Required]
        [Display(Name = "Maximum Score")]
        public int MaximumScore { get; set; }

        [Required]
        [Display(Name = "Assessment Result Label")]
        public string AssessmentResultLabel { get; set; }

        [Required]
        [Display(Name = "Percentage Score")]
        public string PercentageScore { get; set; }


        //Navigation Property
        [ForeignKey("Candidate")]
        public virtual Candidate Candidate { get; set; }

        [ForeignKey("Topic")]
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
