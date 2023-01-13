using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamAssignment4A.Models.JointTables
{
    public class ExamStem
    {

        [Key]
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        [Key]
        [ForeignKey("Stem")]
        public int StemId { get; set; }
        public Stem Stem { get; set; }

        [Required]
        [Display(Name ="Submited Answer")]
        public char SubmittedAnswer { get; set; }

    }
}