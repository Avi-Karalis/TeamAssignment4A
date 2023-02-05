using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    internal interface ITopicService 
    {
        Task<MyDTO> Get(int id);
        Task<IEnumerable<TopicDto>?> GetAll();
        Task<MyDTO> Add(int id, TopicDto topicDto);
        Task<MyDTO> GetForUpdate(int id);
        Task<MyDTO> Update(int id, TopicDto topicDto);        
        Task<MyDTO> GetForDelete(int id);
        Task<MyDTO> Delete(int id);
    }
}