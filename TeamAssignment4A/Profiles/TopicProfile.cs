using AutoMapper;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Profiles
{
    public class TopicProfile: Profile
    {
        public TopicProfile()
        {
            CreateMap<Topic,TopicDto>();

            CreateMap<Topic, TopicDto>()

                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NumberOfPossibleMarks, opt => opt.MapFrom(src => src.NumberOfPossibleMarks))
                .ForMember(dest => dest.TitleOfCertificate, opt => opt.MapFrom(src => src.Certificate.TitleOfCertificate)).ReverseMap();
               


            CreateMap<Stem, StemDto>();

            CreateMap<Stem, StemDto>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.OptionA, opt => opt.MapFrom(src => src.OptionA))
                .ForMember(dest => dest.OptionB, opt => opt.MapFrom(src => src.OptionB))
                .ForMember(dest => dest.OptionC, opt => opt.MapFrom(src => src.OptionC))
                .ForMember(dest => dest.OptionD, opt => opt.MapFrom(src => src.OptionD))
                .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => src.CorrectAnswer))
                .ForMember(dest => dest.TopicDescription, opt => opt.MapFrom(src => src.Topic.Description)).ReverseMap();

            
        }
    }
}
