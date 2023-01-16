using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace TeamAssignment4A.Models.JointTables 
{
   
    public class Score 
    {
        [Key]
        public int Id { get; set; }       
        public int ScorePerTopic { get; set; }

        // Navigation Properties
        public virtual ExamTopic ExamTopic { get; set; }
        public virtual ExamStem ExamStem { get; set; }

    }
}
