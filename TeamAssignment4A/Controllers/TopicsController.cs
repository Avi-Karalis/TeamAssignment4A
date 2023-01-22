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

namespace TeamAssignment4A.Controllers
{
    public class TopicsController : Controller
    {
        private readonly WebAppDbContext _context;
        private readonly IMapper _mapper;

        public TopicsController(WebAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Topics
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Index() // index
        {
            var topics = await _context.Topics.Include(top => top.Certificate).ToListAsync();
            var topicDtos = _mapper.Map<List<TopicDto>>(topics);
            return View(topicDtos);
        }




        // GET: Topics/Details/5
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.FirstOrDefaultAsync(m => m.Id == id);

            TopicDto topicDto = _mapper.Map<TopicDto>(topic);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topicDto);
        }


        
        // GET: Topics/Create
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public IActionResult Create()
        {
            return View();
        }

       // POST: Topics/Create
       
        [HttpPost]
        [ProducesResponseType(typeof(TopicDto), 200)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<TopicDto>> Create([Bind("Id,Description,NumberOfPossibleMarks,TitleOfCertificate")] TopicDto topicDto)
        {
            Certificate cert = _context.Certificates.FirstOrDefault(cert => cert.TitleOfCertificate == topicDto.TitleOfCertificate);
            Topic topic = new Topic
            {
                Id = topicDto.Id,
                Description = topicDto.Description,
                NumberOfPossibleMarks = topicDto.NumberOfPossibleMarks,
                Certificate = cert 
            };
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
            
            
            var topics = await _context.Topics.Include(top => top.Certificate).ToListAsync();
            var topicDtos = _mapper.Map<List<TopicDto>>(topics);

            return View("Index", topicDtos);
            
        }

        // GET: Topics/Edit/5
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.Include(top => top.Certificate).FirstOrDefaultAsync(top => top.Id == id);
            TopicDto topicDto = _mapper.Map<TopicDto>(topic);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topicDto);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,NumberOfPossibleMarks,TitleOfCertificate")] TopicDto topicDto)
        {
            if (id != topicDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    Topic topic = _mapper.Map<Topic>(topicDto);
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topicDto.Id))
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
            return View(topicDto);
        }

        // GET: Topics/Delete/5
        [HttpGet]
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.Include(top => top.Certificate).FirstOrDefaultAsync(top => top.Id == id);
            TopicDto topicDto = _mapper.Map<TopicDto>(topic);
            
            if (topic == null)
            {
                return NotFound();
            }

            return View(topicDto);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]        
        [ProducesResponseType(typeof(TopicDto), 200)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Topics == null)
            {
                return Problem("Entity set 'WebAppDbContext.Topics'  is null.");
            }
            var topic = await _context.Topics.Include(top => top.Certificate).FirstOrDefaultAsync(top => top.Id == id);
            //TopicDto topicDto = _mapper.Map<TopicDto>(topic);
            
            if (topic != null)
            {
                _context.Topics.Remove(topic);
            }

            await _context.SaveChangesAsync();
            var topics = await _context.Topics.Include(top => top.Certificate).ToListAsync();
            var topicDtos = _mapper.Map<List<TopicDto>>(topics);
            return View("Index", topicDtos);
        }

        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }
    }
}
