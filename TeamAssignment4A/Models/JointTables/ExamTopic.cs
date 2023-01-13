using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models.JointTables {
    //[PrimaryKey("TopicId", "ExamId")]
    public class ExamTopic {

        public int ExamTopicId { get; set; }

        //[Key, Column(Order = 0)]
        //public int ExamId { get; set; }

        //[Key, Column(Order = 1)]
        //public int TopicId { get; set; }

        public virtual Exam Exams { get; set; }
        public virtual Topic Topics { get; set; }

    }
}
