using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using TeamAssignment4A.Models.JointTables;

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

       
        //Navigation Property
        public virtual Certificate Certificate { get; set; }

        //public virtual ICollection<Certificate> Certificates { get; set; }
        //public virtual ICollection<Exam> Exams { get; set; }
        //public virtual ICollection<Stem> Stems { get; set; }
    }
}
