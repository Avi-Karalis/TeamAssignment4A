using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Dtos
{
    public class StemDto
    {        
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

        [Required]
        public string TopicDescription { get; set; }
        public Topic Topic { get; set; }
    }
}
