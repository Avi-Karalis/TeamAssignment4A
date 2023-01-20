using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TeamAssignment4A.Dtos
{
    public class StemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string OptionA { get; set; }

        [Required]
        public string OptionB { get; set; }

        [Required]
        public string OptionC { get; set; }

        [Required]
        public string OptionD { get; set; }

        [Required]
        public char CorrectAnswer { get; set; }

        public string TopicDescription { get; set; }
    }
}
