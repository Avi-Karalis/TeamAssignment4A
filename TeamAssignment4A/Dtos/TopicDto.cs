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

        public virtual Certificate Certificate { get; set; }
        public virtual ICollection<StemTopicDto>? Stems { get; set; }
        public virtual ICollection<ExamTopic>? ExamTopics { get; set; }
    }
}
