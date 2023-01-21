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
        private CertificateService _service;
        public CertificatesController(CertificateService service)
        {            
            _service = service;        
        }        

        // GET: Certificates
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllCertificates());
        }

        // GET: Certificates/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            MyDTO myDTO = await _service.GetCertificate(Id);
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
        public async Task<IActionResult> Create(int Id, [Bind("Id,TitleOfCertificate,PassingGrade,MaximumScore")] Certificate certificate)
        {
            MyDTO myDTO = await _service.AddOrUpdateCertificate(Id, certificate);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.Certificates);
            }
            return View($"{myDTO.View}", myDTO.Certificate);
        }

        // GET: Certificates/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            MyDTO myDTO = await _service.GetForUpdate(Id);
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
        public async Task<IActionResult> Edit(int Id, [Bind("Id,TitleOfCertificate,PassingGrade,MaximumScore")] Certificate certificate)
        {
            MyDTO myDTO = await _service.AddOrUpdateCertificate(Id, certificate);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}", myDTO.Certificates);
            }
            return View($"{myDTO.View}", myDTO.Certificate);          
        }

        // GET: Certificates/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {
            MyDTO myDTO = await _service.GetForDelete(Id);
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
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            MyDTO myDTO = await _service.Delete(Id);
            ViewBag.Message = myDTO.Message;
            //if (myDTO.View == "Index")
            //{
            //    return View($"{myDTO.View}", myDTO.Certificates);
            //}
            return View($"{myDTO.View}", myDTO.Certificates);
        }        
    }
}
