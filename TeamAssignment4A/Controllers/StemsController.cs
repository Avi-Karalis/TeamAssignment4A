using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers {
    public class StemsController : Controller 
    {
        private readonly WebAppDbContext _db;
        private readonly StemService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public StemsController(WebAppDbContext db, StemService service, IWebHostEnvironment webHostEnvironment, IMapper mapper) 
        {
            _db = db;
            _service = service;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        // GET: Stems
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Index()
        {            
            return View(await _service.GetAllStems());
        }        

        // GET: Stems/Details/5
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Details(int id)
        {
            MyDTO myDTO = await _service.GetStem(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.StemDtos);
            }
            return View($"{myDTO.View}", myDTO.StemDto);
        }       

        // GET: Stems/Create
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Create() 
        {
            var options = new List<SelectListItem>{
                new SelectListItem { Value = "A", Text = "A" },
                new SelectListItem { Value = "B", Text = "B" },
                new SelectListItem { Value = "C", Text = "C" },
                new SelectListItem { Value = "D", Text = "D" }
            };
            ViewBag.Options = options;
            var topics = await _db.Topics.Include(top => top.Certificate).ToListAsync<Topic>();                               
            var topicDtos = _mapper.Map<List<TopicDto>>(topics);
            var options2 = new SelectList(topicDtos, "Description", "Description");
            ViewBag.Topics = options2;
            return View();
        }

        // POST: Stems/Create
        
        [HttpPost]
        [ProducesResponseType(typeof(StemDto), 200)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer,TopicDescription,Topic")] StemDto stemDto) 
        {
            MyDTO myDTO = await _service.AddOrUpdateStem(id, stemDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.StemDtos);
            }
            return View($"{myDTO.View}", myDTO.StemDto);
        }

        // GET: Stems/Edit/5
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Edit(int id) 
        {
            MyDTO myDTO = await _service.GetForUpdate(id);
            ViewBag.Message = myDTO.Message;
            var options = new List<SelectListItem>{
                new SelectListItem { Value = "A", Text = "A" },
                new SelectListItem { Value = "B", Text = "B" },
                new SelectListItem { Value = "C", Text = "C" },
                new SelectListItem { Value = "D", Text = "D" }
            };
            ViewBag.Options = options;
            var topics = await _db.Topics.Include(top => top.Certificate).ToListAsync<Topic>();
            var topicDtos = _mapper.Map<List<TopicDto>>(topics);
            var options2 = new SelectList(topicDtos, "Description", "Description");
            ViewBag.Topics = options2;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.StemDtos);
            }
            return View($"{myDTO.View}", myDTO.StemDto);
            
            
        }

        // POST: Stems/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer,TopicDescription,Topic")] StemDto stemDto) 
        {
            MyDTO myDTO = await _service.AddOrUpdateStem(id, stemDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.StemDtos);
            }
            return View($"{myDTO.View}", myDTO.StemDto);
        }

        // GET: Stems/Delete/5
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Delete(int id) 
        {
            MyDTO myDTO = await _service.GetForDelete(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.StemDtos);
            }
            return View($"{myDTO.View}", myDTO.StemDto);
        }

        // POST: Stems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            MyDTO myDTO = await _service.Delete(id);
            ViewBag.Message = myDTO.Message;
            return View($"{myDTO.View}", myDTO.StemDtos);
        }
    }
}
