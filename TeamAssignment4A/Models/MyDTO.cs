using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Models
{
    public class MyDTO
    {
        public string? View { get; set; }
        public string? Message { get; set; }
        public CandidateDto? Candidate { get; set; }
        public IEnumerable<CandidateDto>? Candidates { get; set;}
        public Certificate? Certificate { get; set; }
        public IEnumerable<Certificate>? Certificates { get; set;}
        public StemDto? StemDto { get; set; }
        public IEnumerable<StemDto>? StemDtos { get; set; }
        public TopicDto? TopicDto { get; set; }
        public IEnumerable<TopicDto>? TopicDtos { get; set; }
        public ExamDto? ExamDto { get; set; }
        public IEnumerable<ExamDto>? ExamDtos { get; set; }
        public ExamStem? ExamStem { get; set; }
        public IEnumerable<ExamStem>? ExamStems { get; set; }
        public CandidateExam? CandidateExam { get; set; }
        public IEnumerable<CandidateExam>? CandidateExams { get; set; }
        public IEnumerable<CandidateExamStem>? CandidateExamStems { get; set; }

    }
}
