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
        //private UnitOfWork _unit;
        private CertificateService _service;
        public CertificatesController(CertificateService service)
        {            
            //_unit = unit;
            _service = service;
        }
        private bool CertificateExists(int Id)
        {
            return true; //_context.Certificates.Any(e => e.Id == id);
        }
        private bool CertificateDeleted(int Id)
        {
            return true;//_context.Certificates.Any(e => e.Id == id);
        }

        // GET: Certificates
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllCertificates());
        }

        // GET: Certificates/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            MyCertificateDTO myDTO = await _service.GetCertificate(Id);
            return View($"{myDTO.View}, {myDTO.Certificate}");           
        }

        // GET: Certificates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Certificates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, [Bind("Id,TitleOfCertificate,PassingGrade,MaximumScore")] Certificate certificate)
        {            
            return View($"{await _service.AddOrUpdate(Id, certificate)}");            
        }

        // GET: Certificates/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            //if (Id == null || _unit.Certificate == null)
            //{
            //    return NotFound();
            //}

            var certificate = await _service.GetCertificate(Id);
            if (certificate == null)
            {
                return NotFound();
            }
            return View(certificate);
        }

        // POST: Certificates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,TitleOfCertificate,PassingGrade,MaximumScore")] Certificate certificate)
        {           

            if (ModelState .IsValid)
            {
                _service.AddOrUpdate(Id, certificate);
                try
                {
                    //_unit.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificateExists(certificate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View($"{await _service.AddOrUpdate(Id, certificate)}");
        }

        // GET: Certificates/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {
            //if (id == null || _unit.Certificate == null)
            //{
            //    return NotFound();
            //}

            var certificate = await _service.GetCertificate(Id);                
            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_unit.Certificate == null)
            //{
            //    return Problem("Entity set 'WebAppDbContext.Certificates'  is null.");
            //}
            return View();
            //if(await _unit.Certificate.Delete(id))
            //{
            //    _unit.SaveAsync();
            //    //certificateDeleted = true;
            //}
            //else
            //{
            //    //certificateDeleted = false;                
            //}            
            //return RedirectToAction(nameof(Index));
        }        
    }
}
