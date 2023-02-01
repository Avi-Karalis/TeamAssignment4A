using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers
{
    public class AdminExamsController : Controller
    {
        private readonly WebAppDbContext _db;
        private readonly ExamService _service;        

        public AdminExamsController(WebAppDbContext context, ExamService service)
        {
            _db = context;
            _service = service;
        }        

        // GET: Exams

        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }        

        // GET: Exams/Details/5
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Details(int id)
        {
            MyDTO myDTO = await _service.Get(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            return View($"{myDTO.View}", myDTO.ExamDto);
        }        

        // GET: Exams/Create
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public IActionResult Create()
        {            
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");            
            ViewBag.ExamStems = new SelectList(_db.ExamStems, "Id", "Id");
            return View();
        }        

        // POST: Exams/Create
        [HttpPost]
        [ProducesResponseType(typeof(ExamDto), 200)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, 
            [Bind("Id,TitleOfCertificate,Certificate,ExamStemIds,ExamStems")] ExamDto examDto)
        {
            MyDTO myDTO = await _service.AddOrUpdate(id, examDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }            
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            //IEnumerable<int> selections = await _service.GetExamStemIds(examDto);
            ViewBag.ExamStems = new SelectList(_db.ExamStems, "Id", "Id");
            return View($"{myDTO.View}", myDTO.ExamDto);
        }        

        // GET: Exams/Edit/5
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Edit(int id)
        {
            MyDTO myDTO = await _service.GetForUpdate(id);
            ViewBag.Message = myDTO.Message;
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            return View($"{myDTO.View}", myDTO.ExamDto);
        }       

        // POST: Exams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,TitleOfCertificate,Certificate,ExamStemIds,ExamStems")] ExamDto examDto)
        {
            MyDTO myDTO = await _service.AddOrUpdate(id, examDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            return View($"{myDTO.View}", myDTO.ExamDto);
        }        

        // GET: Exams/Delete/5
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            MyDTO myDTO = await _service.GetForDelete(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            return View($"{myDTO.View}", myDTO.ExamDto);
        }        

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            MyDTO myDTO = await _service.Delete(id);
            ViewBag.Message = myDTO.Message;
            return View($"{myDTO.View}", myDTO.ExamDtos);
        }        
    }
}
