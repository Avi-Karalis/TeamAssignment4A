using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class TopicService : ControllerBase, ITopicService
    {
        private WebAppDbContext _db;
        private UnitOfWork _unit;
        private readonly IMapper _mapper;
        private MyDTO _myDTO;
        public TopicService(WebAppDbContext db, UnitOfWork unit, IMapper mapper)
        {
            _db = db;
            _unit = unit;
            _mapper = mapper;
            _myDTO = new MyDTO();
        }
        public async Task<MyDTO> Get(int id)
        {
            if (id == null || _db.Topics == null || await _unit.Topic.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested topic could not be found. Please try again later.";
            }
            else
            {
                _myDTO.View = "Details";
                Topic topic = await _unit.Topic.GetAsync(id);
                _myDTO.TopicDto = _mapper.Map<TopicDto>(topic);
            }
            return _myDTO;
        }
        public async Task<IEnumerable<TopicDto>?> GetAll()
        {
            var topics = await _unit.Topic.GetAllAsync();
            _myDTO.TopicDtos = _mapper.Map<List<TopicDto>>(topics);
            return _myDTO.TopicDtos;
        }

        public async Task<MyDTO> GetForUpdate(int id)
        {
            _myDTO.View = "Edit";
            if (id == null || _db.Topics == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested topic could not be found. Please try again later.";
                return _myDTO;
            }
            Topic topic = await _unit.Topic.GetAsync(id);
            _myDTO.TopicDto = _mapper.Map<TopicDto>(topic);
            if (_myDTO.TopicDto == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested topic could not be found. Please try again later.";
            }
            return _myDTO;
        }

        public async Task<MyDTO> AddOrUpdate(int id, [Bind("Id,Description,NumberOfPossibleMarks,TitleOfCertificate,Certificate")] TopicDto topicDto)
        {
            Certificate certificate = await _unit.Certificate.GetAsyncByTilteOfCert(topicDto.TitleOfCertificate);
            topicDto.Certificate = certificate;
            Topic topic = _mapper.Map<Topic>(topicDto);

            EntityState state = _unit.Topic.AddOrUpdate(topic);
            if (id != topic.Id)
            {
                if (state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "The topic Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.TopicDto = topicDto;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested topic has been added successfully.";
                if (state == EntityState.Modified)
                {
                    _myDTO.Message = "The requested topic has been updated successfully.";
                }
                if (state == EntityState.Modified && !await _unit.Topic.Exists(topic.Id))
                {
                    _myDTO.Message = "The requested topic could not be found. Please try again later.";
                }
                if (await _unit.Topic.DescriptionExists(topic.Id, topic.Description))
                {
                    if (state == EntityState.Added)
                    {
                        _myDTO.View = "Create";
                    }
                    if (state == EntityState.Modified)
                    {
                        _myDTO.View = "Edit";
                    }
                    _myDTO.Message = "This topic description already exists. Please try providing a different description.";
                    _myDTO.TopicDto = topicDto;                    
                    return _myDTO;
                }
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                IEnumerable<Topic> topics = await _unit.Topic.GetAllAsync();
                _myDTO.TopicDtos = _mapper.Map<List<TopicDto>>(topics);
                return _myDTO;
            }
            else
            {
                if (state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.TopicDto = topicDto;
            }
            return _myDTO;
        }

        public async Task<MyDTO> GetForDelete(int id)
        {
            _myDTO.View = "Delete";
            if (id == null || _db.Topics == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested topic could not be found. Please try again later.";
                IEnumerable<Topic> topics = await _unit.Topic.GetAllAsync();
                _myDTO.TopicDtos = _mapper.Map<List<TopicDto>>(topics);
                return _myDTO;
            }
            Topic topic = await _unit.Topic.GetAsync(id);
            _myDTO.TopicDto = _mapper.Map<TopicDto>(topic);
            if (_myDTO.TopicDto == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested topic could not be found. Please try again later.";
                IEnumerable<Topic> topics = await _unit.Topic.GetAllAsync();
                _myDTO.TopicDtos = _mapper.Map<List<TopicDto>>(topics);
            }
            return _myDTO;
        }

        public async Task<MyDTO> Delete(int id)
        {
            _myDTO.View = "Index";
            _myDTO.Message = "The requested topic has been deleted successfully.";
            if (!await _unit.Topic.Exists(id))
            {
                _myDTO.Message = "The requested topic could not be found. Please try again later.";
                return _myDTO;
            }
            Topic topic = await _unit.Topic.GetAsync(id);
            _unit.Topic.Delete(topic);
            await _unit.SaveAsync();
            IEnumerable<Topic> topics = await _unit.Topic.GetAllAsync();
            _myDTO.TopicDtos = _mapper.Map<List<TopicDto>>(topics);
            return _myDTO;
        }
    }    
}
