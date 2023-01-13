using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamAssignment4A.Models.JointTables {
    public class ExamTopic {

        [Key]
        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        [Key]
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Topic Exam { get; set; }
    }
}
