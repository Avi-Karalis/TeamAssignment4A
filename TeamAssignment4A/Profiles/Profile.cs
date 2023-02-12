using AutoMapper;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Profiles
{
    public class Profile: AutoMapper.Profile
    {        
        public Profile()
        {
            CreateMap<Candidate, CandidateDto>();

            CreateMap<Candidate, CandidateDto>()

                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.CountryOfResidence, opt => opt.MapFrom(src => src.CountryOfResidence))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.LandlineNumber, opt => opt.MapFrom(src => src.LandlineNumber))
                .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Address2))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Town, opt => opt.MapFrom(src => src.Town))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.PhotoIdType, opt => opt.MapFrom(src => src.PhotoIdType))
                .ForMember(dest => dest.PhotoIdNumber, opt => opt.MapFrom(src => src.PhotoIdNumber))
                .ForMember(dest => dest.PhotoIdDate, opt => opt.MapFrom(src => src.PhotoIdDate))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.IdentityUser))
                .ForMember(dest => dest.CandidateExams, opt => opt.MapFrom(src => src.CandidateExams))
                .ReverseMap();


            CreateMap<Topic,TopicDto>();

            CreateMap<Topic, TopicDto>()

                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NumberOfPossibleMarks, opt => opt.MapFrom(src => src.NumberOfPossibleMarks))
                .ForMember(dest => dest.TitleOfCertificate, opt => opt.MapFrom(src => src.Certificate.TitleOfCertificate))
                .ForMember(dest => dest.Certificate, opt => opt.MapFrom(src => src.Certificate))
                .ForMember(dest => dest.Stems, opt => opt.MapFrom(src => src.Stems))
                .ReverseMap();
               


            CreateMap<Stem, StemDto>();

            CreateMap<Stem, StemDto>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.OptionA, opt => opt.MapFrom(src => src.OptionA))
                .ForMember(dest => dest.OptionB, opt => opt.MapFrom(src => src.OptionB))
                .ForMember(dest => dest.OptionC, opt => opt.MapFrom(src => src.OptionC))
                .ForMember(dest => dest.OptionD, opt => opt.MapFrom(src => src.OptionD))
                .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => src.CorrectAnswer))
                .ForMember(dest => dest.TopicDescription, opt => opt.MapFrom(src => src.Topic.Description))
                .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Topic))
                .ReverseMap();


            CreateMap<Exam, ExamDto>();

            CreateMap<Exam, ExamDto>()
                .ForMember(dest => dest.TitleOfCertificate, opt => opt.MapFrom(src => src.Certificate.TitleOfCertificate))
                .ForMember(dest => dest.Certificate, opt => opt.MapFrom(src => src.Certificate))
                .ForMember(dest => dest.ExamStems, opt => opt.MapFrom(src => src.ExamStems))
                .ForMember(dest => dest.Stems, opt => opt.MapFrom(src => src.ExamStems.Select(exs => exs.Stem)))
                .ReverseMap();

            CreateMap<Stem, ExamStem>();            

            CreateMap<Stem, ExamStem>()
                .ForPath(dest => dest.Stem.Question, opt => opt.MapFrom(src => src.Question))
                .ForPath(dest => dest.Stem.OptionA, opt => opt.MapFrom(src => src.OptionA))
                .ForPath(dest => dest.Stem.OptionB, opt => opt.MapFrom(src => src.OptionB))
                .ForPath(dest => dest.Stem.OptionC, opt => opt.MapFrom(src => src.OptionC))
                .ForPath(dest => dest.Stem.OptionD, opt => opt.MapFrom(src => src.OptionD))
                .ForPath(dest => dest.Stem.CorrectAnswer, opt => opt.MapFrom(src => src.CorrectAnswer))
                .ForPath(dest => dest.Stem.Topic, opt => opt.MapFrom(src => src.Topic))
                .ReverseMap();
            
            CreateMap<ExamStem, CandidateExamStem>();

            CreateMap<ExamStem, CandidateExamStem>()
                .ForPath(dest => dest.ExamStem.Stem.Question, opt => opt.MapFrom(src => src.Stem.Question))
                .ForPath(dest => dest.ExamStem.Stem.OptionA, opt => opt.MapFrom(src => src.Stem.OptionA))
                .ForPath(dest => dest.ExamStem.Stem.OptionB, opt => opt.MapFrom(src => src.Stem.OptionB))
                .ForPath(dest => dest.ExamStem.Stem.OptionC, opt => opt.MapFrom(src => src.Stem.OptionC))
                .ForPath(dest => dest.ExamStem.Stem.OptionD, opt => opt.MapFrom(src => src.Stem.OptionD))
                .ForPath(dest => dest.ExamStem.Stem.CorrectAnswer, opt => opt.MapFrom(src => src.Stem.CorrectAnswer))
                .ReverseMap();
        }
    }
}
