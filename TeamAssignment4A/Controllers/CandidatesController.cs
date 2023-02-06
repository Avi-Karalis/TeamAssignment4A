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
    [Authorize(Roles = "Admin")]
    public class CandidatesController : Controller
    {
        private readonly CandidateService _service;
        private MyDTO _myDTO;
        public CandidatesController(CandidateService service)
        {
            _service = service;
            _myDTO= new MyDTO();
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(int id)
        {
            _myDTO = await _service.Get(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Candidates);
            }
            return View($"{_myDTO.View}", _myDTO.Candidate);
        }

        // GET: Candidates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,FirstName,MiddleName,LastName,Gender,NativeLanguage,CountryOfResidence," +
            "Birthdate,Email,LandlineNumber,MobileNumber,Address1,Address2,PostalCode,Town,Province,PhotoIdType,PhotoIdNumber," +
            "PhotoIdDate")] Candidate candidate)
        {
            _myDTO = await _service.AddOrUpdate(id, candidate);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Candidates);
            }
            return View($"{_myDTO.View}", _myDTO.Candidate);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            _myDTO = await _service.GetForUpdate(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Candidates);
            }
            return View($"{_myDTO.View}", _myDTO.Candidate);
        }

        // POST: Candidates/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,Gender,NativeLanguage,CountryOfResidence,Birthdate,Email,LandlineNumber,MobileNumber,Address1,Address2,PostalCode,Town,Province,PhotoIdType,PhotoIdNumber,PhotoIdDate")] Candidate candidate)
        {
            _myDTO = await _service.AddOrUpdate(id, candidate);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Candidates);
            }
            return View($"{_myDTO.View}", _myDTO.Candidates);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            _myDTO = await _service.GetForDelete(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}", _myDTO.Candidates);
            }
            return View($"{_myDTO.View}", _myDTO.Candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _myDTO = await _service.Delete(id);
            ViewBag.Message = _myDTO.Message;
            return View($"{_myDTO.View}", _myDTO.Candidates);
        }        
    }
}
