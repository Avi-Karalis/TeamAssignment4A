using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TeamAssignment4A.Models.JointTables;
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

    }
}
