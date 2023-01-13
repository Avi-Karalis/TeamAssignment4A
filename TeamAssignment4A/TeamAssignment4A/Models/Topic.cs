using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Models
{
    public class Topic {
        [Key]
        [Required]
        [Display(Name ="Topic Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Number Of Possible Marks")]
        public int NumberOfPossibleMarks { get; set; }

        [Required]
        [Display(Name = "Certificate ID")]
        public int CertificateID { get; set; }

        //Navigation Property
        
        public virtual ICollection<Certificate> Certificates { get; set; }

        //Navigation Property
        public virtual ICollection<TopicStem> TopicStems { get; set; }
    }
}
