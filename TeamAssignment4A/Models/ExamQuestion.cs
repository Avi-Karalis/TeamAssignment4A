using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TeamAssignment4A.Models
{
    public class ExamQuestion
    {

        [Required]
        [Display(Name = "Exam Id")]
        public int CandidateExamId { get; set; }

        [Required]
        [Display(Name = "Stem Id")]
        public int StemId { get; set; }


        [Required]
        [Display(Name = "Question")]
        public string Question { get; set; }

        [Required]
        [Display(Name = "Option A")]
        public string OptionA { get; set; }

        [Required]
        [Display(Name = "Option B")]
        public string OptionB { get; set; }

        [Required]
        [Display(Name = "Option C")]
        public string OptionC { get; set; }

        [Required]
        [Display(Name = "Option D")]
        public string OptionD { get; set; }

        [Display(Name = "User Answer")]

        public char Answer { get; set; }

        public ExamQuestion()
        {

        }

        public ExamQuestion(Stem stem, int examId)
        {
            CandidateExamId = examId;
            StemId = stem.Id;
            Question = stem.Question;
            OptionA = stem.OptionA;
            OptionB = stem.OptionB;
            OptionC = stem.OptionC;
            OptionD = stem.OptionD;
        }
    }
}
