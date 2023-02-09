using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;
using TeamAssignment4A.Services;

namespace TeamAssignment4A.Controllers {
    [Authorize(Roles = "Admin, Candidate")]
    public class EShopController : Controller 
    {
        
        
        private readonly EShopService _service;
        private MyDTO _myDTO;
        public EShopController(EShopService service) 
        {
            _service = service;
            _myDTO = new MyDTO();
        }

        // GET: Eshop
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: EShop/Certificates List
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(Certificate), 200)]
        public async Task<IActionResult> ListOfCertificates() 
        {
            return View(await _service.GetAll());
        }

        // GET: EShop/CertificatesList/Details/5
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(Certificate), 200)]
        public async Task<IActionResult> Details(int id) 
        {
            _myDTO = await _service.GetCert(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "ListOfCertificates")
            {
                return View($"{_myDTO.View}", _myDTO.Certificates);
            }
            return View($"{_myDTO.View}", _myDTO.Certificate);
        }

        // GET: EShop/BuyExamVoucher
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> BuyExamVoucher(int id) 
        {
            _myDTO = await _service.GetExam(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}");
            }
            else if(_myDTO.View == "ListOfCertificates")
            {
                return View($"{_myDTO.View}", _myDTO.Certificates);
            }
            // Last return has _myDTO.View = "BuyExamVoucher"
            return View($"{_myDTO.View}", _myDTO.CandidateExam);
        }

        // POST: EShop/BuyExamVoucher

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> BuyExamVoucher(int id,
            [Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
                "CandidateScore,PercentageScore,AssessmentResultLabel,MarkerUserName," +
                "Candidate,Exam,CandidateExamStems")] CandidateExam candidateExam) 
        {
            _myDTO = await _service.BuyExam(id, candidateExam);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "Index")
            {
                return View($"{_myDTO.View}");
            }
            return View($"{_myDTO.View}", _myDTO.CandidateExam);
        }

        // GET: EShop/BookedExams
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> BookedExams()
        {
            return View(await _service.GetBooked());
        }

        // GET: EShop/ChangeDate/5
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> ChangeDate(int id) 
        {
            MyDTO myDTO = await _service.GetForUpdate(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "BookedExams")
            {
                return View($"{myDTO.View}", myDTO.CandidateExams);
            }
            // Last return has _myDTO.View = "ChangeDate"
            return View($"{myDTO.View}", myDTO.CandidateExam);
        }
        
        // POST: EShop/ChangeDate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> ChangeDate(int id,
            [Bind("Id,AssessmentTestCode,ExaminationDate,ScoreReportDate," +
                "CandidateScore,PercentageScore,AssessmentResultLabel,MarkerUserName," +
                "Candidate,Exam,CandidateExamStems")] CandidateExam candidateExam) 
        {
            MyDTO myDTO = await _service.UpdateDate(id, candidateExam);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "Index")
            {
                return View($"{myDTO.View}");
            }
            else if(myDTO.View == "ChangeDate")
            {
                return View($"{myDTO.View}", myDTO.CandidateExam);
            }
            // Last return has _myDTO.View = "BookedExams"
            return View($"{myDTO.View}", myDTO.CandidateExams);
        }

        // GET: EShop/Delete/5
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            MyDTO myDTO = await _service.GetForDelete(id);
            ViewBag.Message = myDTO.Message;
            if (myDTO.View == "BookedExams")
            {
                return View($"{myDTO.View}", _myDTO.CandidateExams);
            }
            // Last return has _myDTO.View = "Delete"
            return View($"{myDTO.View}", myDTO.CandidateExam);
        }

        // POST: EShop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            MyDTO myDTO = await _service.Delete(id);
            ViewBag.Message = myDTO.Message;
            // Return has _myDTO.View = "BookedExams"
            return View($"{myDTO.View}", myDTO.CandidateExams);
        }

        // GET: EShop/MarkedCertifications
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> MarkedCertifications()
        {
            _myDTO = await _service.GetMarkedExams();
            ViewBag.Message = _myDTO.Message;
            return View(_myDTO.CandidateExams);
        }

        // GET: EShop/MarkedCertifications/Details/5
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(CandidateExam), 200)]
        public async Task<IActionResult> ExamDetails(int id)
        {
            _myDTO = await _service.GetMarkedExam(id);
            ViewBag.Message = _myDTO.Message;
            if (_myDTO.View == "MarkedCertifications")
            {
                return View($"{_myDTO.View}", _myDTO.CandidateExams);
            }
            // Last return has _myDTO.View = "ExamDetails"
            return View($"{_myDTO.View}", _myDTO.CandidateExam);
        }
    }
}