using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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

        // GET: Exams/Create(TitleOfCert)
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public IActionResult Create()
        {            
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");                        
            return View();
        }

        // POST: Exams/Create(TitleOfCert)
        [HttpPost]
        [ProducesResponseType(typeof(ExamDto), 200)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,TitleOfCertificate," +
            "Certificate,StemIds,Stems,ExamStemIds,ExamStems")] ExamDto examDto)
        {
            MyDTO myDTO = await _service.AddCert(id, examDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "CreateExamStems")
            {
                ViewBag.Certificate = examDto.Certificate;
                ViewBag.StemIds = new SelectList(await _service.GetStemIds(examDto));
                return RedirectToAction($"{myDTO.View}", myDTO.ExamDto);
            }  
            return View($"{myDTO.View}", myDTO.ExamDto);
        }

        // GET: Exams/Create(ExamStems)
        [HttpGet]
        [ProducesResponseType(typeof(ExamDto), 200)]
        public async Task<IActionResult> CreateExamStems(ExamDto examDto)
        {
            MyDTO myDTO = await _service.GetByExam(examDto);
            ViewBag.Message = myDTO.Message;            
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            ViewBag.StemIds = new SelectList(myDTO.ExamDto.StemIds);
            //ViewBag.StemIds = new SelectList(await _service.GetStemIds(myDTO.ExamDto));
            return View($"{myDTO.View}", myDTO.ExamDto);            
        }

        // POST: Exams/Create(ExamStems)
        [HttpPost]
        [ProducesResponseType(typeof(ExamDto), 200)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExamStems(int id, [Bind("Id,TitleOfCertificate," +
            "Certificate,StemIds,Stems,ExamStemIds,ExamStems")] ExamDto examDto)
        {
            MyDTO myDTO = await _service.AddStems(id, examDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            ViewBag.StemIds = new SelectList(await _service.GetStemIds(examDto));
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
            //IEnumerable<int> selections = await _service.GetExamStemIds(myDTO.ExamDto);
            ViewBag.Stems = new SelectList(await _service.GetStemIds(myDTO.ExamDto));
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
            [Bind("Id,TitleOfCertificate,Certificate,StemIds,Stems,ExamStemIds,ExamStems")] ExamDto examDto)
        {
            MyDTO myDTO = await _service.Update(id, examDto);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.ExamDtos);
            }
            ViewBag.Certificates = new SelectList(_db.Certificates, "TitleOfCertificate", "TitleOfCertificate");
            //IEnumerable<int> selections = await _service.GetExamStemIds(myDTO.ExamDto);
            ViewBag.ExamStems = new SelectList(await _service.GetStemIds(myDTO.ExamDto));
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
