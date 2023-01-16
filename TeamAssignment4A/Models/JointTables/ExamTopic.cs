using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models.JointTables 
{
    
    public class ExamTopic 
    {
        public int ExamTopicId { get; set; }

        // Navigation Properties
        public virtual Exam Exam { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
