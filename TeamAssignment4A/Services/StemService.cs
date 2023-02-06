using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class StemService : ControllerBase, IStemService
    {
        private WebAppDbContext _db;
        private UnitOfWork _unit;
        private readonly IMapper _mapper;
        private MyDTO _myDTO;
        public StemService(WebAppDbContext db, UnitOfWork unit, IMapper mapper)
        {
            _db = db;
            _unit = unit;
            _mapper = mapper;
            _myDTO = new MyDTO();
        }
        public async Task<MyDTO> Get(int id)
        {
            if (id == null || _db.Stems == null || await _unit.Stem.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
                var stems = await _unit.Stem.GetAllAsync();
                _myDTO.StemDtos = _mapper.Map<List<StemDto>>(stems);
            }
            else
            {
                _myDTO.View = "Details";
                Stem stem = await _unit.Stem.GetAsync(id);
                _myDTO.StemDto = _mapper.Map<StemDto>(stem);
            }
            return _myDTO;
        }
        
        public async Task<IEnumerable<StemDto>?> GetAll()
        {
            var stems = await _unit.Stem.GetAllAsync();
            _myDTO.StemDtos = _mapper.Map<List<StemDto>>(stems);
            return _myDTO.StemDtos;
        }

        public async Task<IEnumerable<Topic>?> GetTopics()
        {
            return await _unit.Topic.GetAllAsync();
        }

        public async Task<MyDTO> GetForUpdate(int id)
        {
            _myDTO.View = "Edit";
            if (id == null || _db.Stems == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
                var stems = await _unit.Stem.GetAllAsync();
                _myDTO.StemDtos = _mapper.Map<List<StemDto>>(stems);
                return _myDTO;
            }
            Stem stem = await _unit.Stem.GetAsync(id);
            _myDTO.StemDto = _mapper.Map<StemDto>(stem);
            if (_myDTO.StemDto == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
                var stems = await _unit.Stem.GetAllAsync();
                _myDTO.StemDtos = _mapper.Map<List<StemDto>>(stems);
            }
            return _myDTO;
        }

        public async Task<MyDTO> Add(int id, [Bind("Id,Question,OptionA,OptionB,OptionC," +
            "OptionD,CorrectAnswer,TopicDescription,Topic")] StemDto stemDto)
        {            
            Topic topic = await _unit.Topic.GetAsyncByDesc(stemDto.TopicDescription);
            stemDto.Topic = topic;                
            Stem stem = _mapper.Map<Stem>(stemDto);

            _unit.Stem.AddOrUpdate(stem);
            if (id != stem.Id)
            {
                _myDTO.View = "Create";
                _myDTO.Message = "The stem Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.StemDto = stemDto;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested stem has been added successfully.";
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                IEnumerable<Stem> stems = await _unit.Stem.GetAllAsync();
                _myDTO.StemDtos = _mapper.Map<List<StemDto>>(stems);
                return _myDTO;
            }
            else
            {
                _myDTO.View = "Create";
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.StemDto = stemDto;
            }
            return _myDTO;
        }

        public async Task<MyDTO> Update(int id, [Bind("Id,Question,OptionA,OptionB,OptionC," +
            "OptionD,CorrectAnswer,TopicDescription,Topic")] StemDto stemDto)
        {
            Stem stem = await _unit.Stem.GetAsync(stemDto.Id);
            stem.Question = stemDto.Question;
            stem.OptionA = stemDto.OptionA;
            stem.OptionB = stemDto.OptionB;
            stem.OptionC = stemDto.OptionC;
            stem.OptionD = stemDto.OptionD;
            stem.CorrectAnswer = stemDto.CorrectAnswer;
            stem.Topic = await _unit.Topic.GetAsyncByDesc(stemDto.TopicDescription);
            _unit.Stem.AddOrUpdate(stem);
            stemDto = _mapper.Map<StemDto>(stem);
            
            if (id != stem.Id)
            {
                _myDTO.View = "Edit";
                _myDTO.Message = "The stem Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.StemDto = stemDto;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested stem has been updated successfully.";
                if (!await _unit.Stem.Exists(stem.Id))
                {
                    _myDTO.Message = "The requested Stem could not be found. Please try again later.";
                }
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                IEnumerable<Stem> stems = await _unit.Stem.GetAllAsync();
                _myDTO.StemDtos = _mapper.Map<List<StemDto>>(stems);
                return _myDTO;
            }
            else
            {
                _myDTO.View = "Edit";
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.StemDto = stemDto;
            }
            return _myDTO;
        }

        public async Task<MyDTO> GetForDelete(int id)
        {
            _myDTO.View = "Delete";
            if (id == null || _db.Stems == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
                IEnumerable<Stem> stems = await _unit.Stem.GetAllAsync();
                _myDTO.StemDtos = _mapper.Map<List<StemDto>>(stems);
                return _myDTO;
            }
            Stem stem = await _unit.Stem.GetAsync(id);
            _myDTO.StemDto = _mapper.Map<StemDto>(stem);
            if (_myDTO.StemDto == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
                IEnumerable<Stem> stems = await _unit.Stem.GetAllAsync();
                _myDTO.StemDtos = _mapper.Map<List<StemDto>>(stems);
            }
            return _myDTO;
        }        

        public async Task<MyDTO> Delete(int id)
        {
            _myDTO.View = "Index";
            _myDTO.Message = "The requested stem has been deleted successfully.";
            if (!await _unit.Stem.Exists(id))
            {
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
                return _myDTO;
            }
            Stem stem = await _unit.Stem.GetAsync(id);
            _unit.Stem.Delete(stem);
            await _unit.SaveAsync();
            IEnumerable<Stem> stems = await _unit.Stem.GetAllAsync();
            _myDTO.StemDtos = _mapper.Map<List<StemDto>>(stems);
            return _myDTO;
        }
    }
}
