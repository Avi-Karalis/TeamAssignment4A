namespace TeamAssignment4A.Dtos {
    public class ManualGradingExamDTO {

        public string Question { get; set; }
        public char SubmittedAnswer { get; set; }
        public char CorrectAnswer { get; set; }
        public string Result { get; set; }
        public int CandidateExamStemId { get; set; }
    }
}
