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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly StemService _service;
        public StemsController(StemService service, WebAppDbContext db, IWebHostEnvironment webHostEnvironment,IMapper mapper) 
        {
            _service = service;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        // GET: Stems
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Index() // index
        {
            //var stems = await _service.GetAllStems();
            //var stemDtos = _mapper.Map<List<StemDto>>(stems);
            return View(await _service.GetAllStems());
        }
        //public async Task<IActionResult> Index() 
        //{
        //    List<Stem> ListOfStems = _context.Stems.Include(s => s.Topic).ToList();
        //    //foreach (var topic in ListOfTopics) {

        //    //    Stem stem = _context.Stems.Find(TopicID);
        //    //}

        //    return View(await _context.Stems.ToListAsync());
        //}

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
        //public async Task<IActionResult> Details(int? id) {
        //    if (id == null || _context.Stems == null) {
        //        return NotFound();
        //    }

        //    var stem = await _context.Stems
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (stem == null) {
        //        return NotFound();
        //    }

        //    return View(stem);
        //}

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
            var topics = await _db.Topics.ToListAsync();
            var topicDtos = _mapper.Map<List<TopicDto>>(topics);

            var options2 = new SelectList(topicDtos, "Id", "Description");
            ViewBag.Topics = options2;
            return View();
        }

        // POST: Stems/Create
        
        [HttpPost]
        [ProducesResponseType(typeof(StemDto), 200)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer,TopicDescription")] StemDto stemDto) 
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer,TopicDescription")] StemDto stemDto) 
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

        //private bool StemExists(int id) {
        //    return _context.Stems.Any(e => e.Id == id);
        //}

    }
}
