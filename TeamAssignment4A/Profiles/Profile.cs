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

            CreateMap<ExamStem, ExamStemDto>();

            CreateMap<ExamStem, ExamStemDto>()
                .ForMember(dest => dest.StemId, opt => opt.MapFrom(src => src.Stem.Id))
                .ForMember(dest => dest.Stem, opt => opt.MapFrom(src => src.Stem))
                .ForMember(dest => dest.ExamId, opt => opt.MapFrom(src => src.Exam.Id))
                .ForMember(dest => dest.Exam, opt => opt.MapFrom(src => src.Exam))
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
        }
    }
}
