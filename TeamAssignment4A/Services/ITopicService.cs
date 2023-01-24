using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    internal interface ITopicService
    {
        Task<MyDTO> GetTopic(int id);
        Task<IEnumerable<TopicDto>?> GetAllTopics();
        Task<MyDTO> AddOrUpdateTopic(int id, TopicDto topicDto);
        Task<MyDTO> Delete(int id);
        Task<MyDTO> GetForUpdate(int id);
        Task<MyDTO> GetForDelete(int id);
    }
}