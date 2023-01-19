using AutoMapper;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Profiles
{
    public class TopicProfile: Profile
    {
        public TopicProfile()
        {
            CreateMap<Topic,TopicDto>().ReverseMap();
            CreateMap<Stem, StemTopicDto>().ReverseMap();
        }
    }
}
