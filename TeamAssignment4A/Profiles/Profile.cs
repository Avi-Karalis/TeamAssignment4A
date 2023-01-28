using AutoMapper;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Profiles
{
    public class Profile: AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<Topic,TopicDto>();

            CreateMap<Topic, TopicDto>()

                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NumberOfPossibleMarks, opt => opt.MapFrom(src => src.NumberOfPossibleMarks))
                .ForMember(dest => dest.TitleOfCertificate, opt => opt.MapFrom(src => src.Certificate.TitleOfCertificate))
                .ForMember(dest => dest.Certificate, opt => opt.MapFrom(src => src.Certificate))
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
                .ForMember(dest => dest.AssessmentTestCode, opt => opt.MapFrom(src => src.AssessmentTestCode))
                .ForMember(dest => dest.ExaminationDate, opt => opt.MapFrom(src => src.ExaminationDate))
                .ForMember(dest => dest.ScoreReportDate, opt => opt.MapFrom(src => src.ScoreReportDate))
                .ForMember(dest => dest.CandidateScore, opt => opt.MapFrom(src => src.CandidateScore))
                .ForMember(dest => dest.PercentageScore, opt => opt.MapFrom(src => src.PercentageScore))
                .ForMember(dest => dest.AssessmentResultLabel, opt => opt.MapFrom(src => src.AssessmentResultLabel))
                .ForMember(dest => dest.CandidateId, opt => opt.MapFrom(src => src.Candidate.Id))
                .ForMember(dest => dest.Candidate, opt => opt.MapFrom(src => src.Candidate))
                .ForMember(dest => dest.TitleOfCertificate, opt => opt.MapFrom(src => src.Certificate.TitleOfCertificate))
                .ForMember(dest => dest.Certificate, opt => opt.MapFrom(src => src.Certificate))
                .ReverseMap();
        }
    }
}
