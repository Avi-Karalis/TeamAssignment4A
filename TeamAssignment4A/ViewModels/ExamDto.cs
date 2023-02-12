using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Dtos
{
    public class ExamDto
    {
        [Display(Name = "Exam Id")]
        public int Id { get; set; }

        [Display(Name = "Title of Certificate")]
        public string TitleOfCertificate { get; set; }
        public Certificate Certificate { get; set; }

        [Required]
        [NotMapped]
        [Display(Name = "Stem Id")]
        public List<int> StemIds { get; set; }

        [Display(Name = "Stems")]
        public List<Stem> Stems { get; set; }

        [NotMapped]
        [Display(Name = "Exam Stem Id")]
        public List<int> ExamStemIds { get; set; }

        [Display(Name = "Exam Stems")]
        public List<ExamStem> ExamStems { get; set; }
    }
}
