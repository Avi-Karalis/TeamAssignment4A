using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamAssignment4A.Models.JointTables
{
    public class TopicStem
    {
        [Key]
        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        [Key]
        [ForeignKey("Stem")]
        public Stem Stem { get; set; }
    }
}