using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers
{
    public class CertificatesController : Controller
    {       
        private readonly CertificateService _service;
        public CertificatesController(CertificateService service)
        {            
            _service = service;        
        }        

        // GET: Certificates
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Certificates/Details/5
        public async Task<IActionResult> Details(int id)
        {
            MyDTO myDTO = await _service.Get(id);
            ViewBag.Message = myDTO.Message;
            if(myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.Certificates);
            }
            return View($"{myDTO.View}", myDTO.Certificate);           
        }

        // GET: Certificates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Certificates/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,TitleOfCertificate,PassingGrade,MaximumScore")] Certificate certificate)
        {
            MyDTO myDTO = await _service.AddOrUpdate(id, certificate);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.Certificates);
            }
            return View($"{myDTO.View}", myDTO.Certificate);
        }

        // GET: Certificates/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            MyDTO myDTO = await _service.GetForUpdate(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.Certificates);
            }
            return View($"{myDTO.View}", myDTO.Certificate);
        }

        // POST: Certificates/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TitleOfCertificate,PassingGrade,MaximumScore")] Certificate certificate)
        {
            MyDTO myDTO = await _service.AddOrUpdate(id, certificate);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.Certificates);
            }
            return View($"{myDTO.View}", myDTO.Certificate);          
        }

        // GET: Certificates/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            MyDTO myDTO = await _service.GetForDelete(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.Certificates);
            }
            return View($"{myDTO.View}", myDTO.Certificate);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            MyDTO myDTO = await _service.Delete(id);
            ViewBag.Message = myDTO.Message;
            return View($"{myDTO.View}", myDTO.Certificates);
        }        
    }
}
