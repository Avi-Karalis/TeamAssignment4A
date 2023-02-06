using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers {
    [Authorize]
    public class StemsController : Controller 
    {
        private readonly StemService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private MyDTO _myDTO;
        
        public StemsController(StemService service, IWebHostEnvironment webHostEnvironment) 
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment; 
            _myDTO = new MyDTO();
        }

        // GET: Stems
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Index()
        {            
            return View(await _service.GetAll());
        }        

        // GET: Stems/Details/5
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Details(int id)
        {
            _myDTO = await _service.Get(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.StemDtos);
            }
            return View($"{_myDTO.View}", _myDTO.StemDto);
        }       

        // GET: Stems/Create
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Create() 
        {
            ViewBag.Options = new List<SelectListItem>{
                new SelectListItem { Value = "A", Text = "A" },
                new SelectListItem { Value = "B", Text = "B" },
                new SelectListItem { Value = "C", Text = "C" },
                new SelectListItem { Value = "D", Text = "D" }
            };                        
            ViewBag.Topics = new SelectList(await _service.GetTopics(), "Description", "Description");
            return View();
        }

        // POST: Stems/Create
        
        [HttpPost]
        [ProducesResponseType(typeof(StemDto), 200)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer,TopicDescription,Topic")] StemDto stemDto) 
        {
            _myDTO = await _service.Add(id, stemDto);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.StemDtos);
            }
            ViewBag.Topics = new SelectList(await _service.GetTopics(), "Description", "Description");
            return View($"{_myDTO.View}", _myDTO.StemDto);
        }

        // GET: Stems/Edit/5
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Edit(int id) 
        {
            _myDTO = await _service.GetForUpdate(id);
            ViewBag.Message = _myDTO.Message;
            ViewBag.Options = new List<SelectListItem>{
                new SelectListItem { Value = "A", Text = "A" },
                new SelectListItem { Value = "B", Text = "B" },
                new SelectListItem { Value = "C", Text = "C" },
                new SelectListItem { Value = "D", Text = "D" }
            };                                    
            ViewBag.Topics = new SelectList(await _service.GetTopics(), "Description", "Description");
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.StemDtos);
            }
            return View($"{_myDTO.View}", _myDTO.StemDto);
            
            
        }

        // POST: Stems/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer,TopicDescription,Topic")] StemDto stemDto) 
        {
            _myDTO = await _service.Update(id, stemDto);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.StemDtos);
            }
            ViewBag.Topics = new SelectList(await _service.GetTopics(), "Description", "Description");
            return View($"{_myDTO.View}", _myDTO.StemDto);
        }

        // GET: Stems/Delete/5
        [HttpGet]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> Delete(int id) 
        {
            _myDTO = await _service.GetForDelete(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.StemDtos);
            }
            return View($"{_myDTO.View}", _myDTO.StemDto);
        }

        // POST: Stems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(StemDto), 200)]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            _myDTO = await _service.Delete(id);
            ViewBag.Message = _myDTO.Message;
            return View($"{_myDTO.View}", _myDTO.StemDtos);
        }
    }
}
