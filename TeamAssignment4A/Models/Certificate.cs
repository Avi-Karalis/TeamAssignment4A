using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamAssignment4A.Models.JointTables;
//using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Models
{
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
        [Display(Name = "Passing Grade")]
        public int PassingGrade { get; set; }

        [Required]
        [Display(Name = "Maximum Score")]
        public int MaximumScore { get; set; }


        // Navigation Property
        //public virtual Topic Topic { get; set; }
        //public virtual Exam Exam { get; set; }

        // Navigation Property

        //public virtual ICollection<ExamTopic> ExamTopics { get; set; }



    }
}
