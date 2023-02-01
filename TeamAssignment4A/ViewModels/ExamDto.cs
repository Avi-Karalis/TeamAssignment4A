using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Exam Stem Id")]
        public IEnumerable<int> ExamStemIds { get; set; }
        [Display(Name = "Exam Stems")]
        public IEnumerable<ExamStem> ExamStems { get; set; }
    }
}
