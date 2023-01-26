using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    internal interface ITopicService : IGenericService<TopicDto>
    {
        new Task<IEnumerable<TopicDto>?> GetAll();
        new Task<MyDTO> AddOrUpdate(int id, TopicDto topicDto);        
    }
}