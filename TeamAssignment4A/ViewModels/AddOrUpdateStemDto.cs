using TeamAssignment4A.Models;

namespace TeamAssignment4A.ViewModels
{
    public class AddOrUpdateStemDto
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string OptionA { get; set; }

        public string OptionB { get; set; }

        public string OptionC { get; set; }

        
        public string OptionD { get; set; }

        
        public char CorrectAnswer { get; set; }

        public Topic Topic { get; set; }
    }
}
