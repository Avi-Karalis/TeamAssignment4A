using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class TopicsController : Controller
    {
        private readonly WebAppDbContext _db;
        private readonly TopicService _service;        

        public TopicsController(WebAppDbContext context, TopicService service)
        {
            _db = context;
            _service = service;             
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
            MyDTO myDTO = await _service.Get(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.TopicDtos);
            }
            return View($"{myDTO.View}", myDTO.TopicDto);            
        }
        
        // GET: Topics/Create
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public IActionResult Create()
        {            
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            return View();
        }

       // POST: Topics/Create
       
        [HttpPost]
        [ProducesResponseType(typeof(TopicDto), 200)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int id, [Bind("Id,Description,NumberOfPossibleMarks,TitleOfCertificate,Certificate")] TopicDto topicDto)
        {
            MyDTO myDTO = await _service.AddOrUpdate(id, topicDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.TopicDtos);
            }
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            return View($"{myDTO.View}", myDTO.TopicDto);           
        }

        // GET: Topics/Edit/5
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Edit(int id)
        {
            MyDTO myDTO = await _service.GetForUpdate(id);
            ViewBag.Message = myDTO.Message;            
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.TopicDtos);
            }
            return View($"{myDTO.View}", myDTO.TopicDto);            
        }

        // POST: Topics/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]        
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,NumberOfPossibleMarks,TitleOfCertificate,Certificate")] TopicDto topicDto)
        {
            MyDTO myDTO = await _service.AddOrUpdate(id, topicDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.TopicDtos);
            }
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            return View($"{myDTO.View}", myDTO.TopicDto);            
        }

        // GET: Topics/Delete/5
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            MyDTO myDTO = await _service.GetForDelete(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.TopicDtos);
            }
            return View($"{myDTO.View}", myDTO.TopicDto);            
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]        
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            MyDTO myDTO = await _service.Delete(id);
            ViewBag.Message = myDTO.Message;
            return View($"{myDTO.View}", myDTO.TopicDtos);            
        }       
    }
}
