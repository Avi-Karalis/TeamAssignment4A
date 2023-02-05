using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers
{
    [Authorize]
    public class TopicsController : Controller
    {
        private readonly WebAppDbContext _db;
        private readonly TopicService _service;
        private MyDTO _myDTO;

        public TopicsController(WebAppDbContext context, TopicService service)
        {
            _db = context;
            _service = service;
            _myDTO= new MyDTO();
        }

        // GET: Topics
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());        
        }

        // GET: Topics/Details/5
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Details(int id)
        {
            _myDTO = await _service.Get(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.TopicDtos);
            }
            return View($"{_myDTO.View}", _myDTO.TopicDto);            
        }
        
        // GET: Topics/Create
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Create()
        {            
            ViewBag.Certificates = new SelectList(await _service.GetCerts(), "TitleOfCertificate", "TitleOfCertificate");
            return View();
        }

       // POST: Topics/Create
       
        [HttpPost]
        [ProducesResponseType(typeof(TopicDto), 200)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,Description,NumberOfPossibleMarks,TitleOfCertificate,Certificate")] TopicDto topicDto)
        {
            _myDTO = await _service.Add(id, topicDto);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.TopicDtos);
            }
            ViewBag.Certificates = new SelectList(await _service.GetCerts(), "TitleOfCertificate", "TitleOfCertificate");
            return View($"{_myDTO.View}", _myDTO.TopicDto);           
        }

        // GET: Topics/Edit/5
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Edit(int id)
        {
            _myDTO = await _service.GetForUpdate(id);
            ViewBag.Message = _myDTO.Message;            
            ViewBag.Certificates = new SelectList(await _service.GetCerts(), "TitleOfCertificate", "TitleOfCertificate");
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.TopicDtos);
            }
            return View($"{_myDTO.View}", _myDTO.TopicDto);            
        }

        // POST: Topics/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]        
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,NumberOfPossibleMarks,TitleOfCertificate,Certificate")] TopicDto topicDto)
        {
            _myDTO = await _service.Update(id, topicDto);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.TopicDtos);
            }
            ViewBag.Certificates = new SelectList(await _service.GetCerts(), "TitleOfCertificate", "TitleOfCertificate");
            return View($"{_myDTO.View}", _myDTO.TopicDto);            
        }

        // GET: Topics/Delete/5
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            _myDTO = await _service.GetForDelete(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.TopicDtos);
            }
            return View($"{_myDTO.View}", _myDTO.TopicDto);            
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]        
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _myDTO = await _service.Delete(id);
            ViewBag.Message = _myDTO.Message;
            return View($"{_myDTO.View}", _myDTO.TopicDtos);            
        }       
    }
}
