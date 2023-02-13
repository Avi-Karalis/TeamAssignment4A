using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers
{
    [Authorize(Roles = "Admin, QA")]
    public class CertificatesController : Controller
    {       
        private readonly CertificateService _service;
        private MyDTO _myDTO;
        public CertificatesController(CertificateService service)
        {            
            _service = service;
            _myDTO= new MyDTO();
        }        

        // GET: Certificates
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Certificates/Details/5
        public async Task<IActionResult> Details(int id)
        {
            _myDTO = await _service.Get(id);
            ViewBag.Message = _myDTO.Message;
            if(_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Certificates);
            }
            return View($"{_myDTO.View}", _myDTO.Certificate);           
        }

        // GET: Certificates/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Certificates/Create        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,TitleOfCertificate,PassingGrade,MaximumScore")] Certificate certificate)
        {
            _myDTO = await _service.AddOrUpdate(id, certificate);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Certificates);
            }
            return View($"{_myDTO.View}", _myDTO.Certificate);
        }

        // GET: Certificates/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            _myDTO = await _service.GetForUpdate(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Certificates);
            }
            return View($"{_myDTO.View}", _myDTO.Certificate);
        }

        // POST: Certificates/Edit/5        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TitleOfCertificate,PassingGrade,MaximumScore")] Certificate certificate)
        {
            _myDTO = await _service.AddOrUpdate(id, certificate);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Certificates);
            }
            return View($"{_myDTO.View}", _myDTO.Certificate);          
        }

        // GET: Certificates/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _myDTO = await _service.GetForDelete(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Certificates);
            }
            return View($"{_myDTO.View}", _myDTO.Certificate);
        }

        // POST: Certificates/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _myDTO = await _service.Delete(id);
            ViewBag.Message = _myDTO.Message;
            return View($"{_myDTO.View}", _myDTO.Certificates);
        }        
    }
}
