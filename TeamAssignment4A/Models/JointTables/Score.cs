using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace TeamAssignment4A.Models.JointTables {
    //[PrimaryKey("ExamTopicsId","ExamStemsId")]
    public class Score {
        [Key]
        public int Id { get; set; } 

        //[ForeignKey("ExamTopic"), Column(Order = 0)]
        //public int ExamTopicsId { get; set; }
        //[ForeignKey("ExamStem"), Column(Order = 1)]
        //public int ExamStemsId { get; set; }

        public virtual ExamTopic ExamTopics { get; set; }
        public virtual ExamStem ExamStems { get; set; }

        public int ScorePerTopic { get; set; }
    }
}
