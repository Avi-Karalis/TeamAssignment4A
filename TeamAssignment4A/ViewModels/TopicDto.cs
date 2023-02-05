using System.ComponentModel.DataAnnotations;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Dtos
{
    public class TopicDto
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int NumberOfPossibleMarks { get; set; }

        [Required]
        public string TitleOfCertificate { get; set; }

        public Certificate Certificate { get; set; }
        public List<Stem> Stems { get; set; }

    }
}
