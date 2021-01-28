using AutoMapper;
using BugTrackerAPI.DataTransferObjects;
using BugTrackerAPI.Entities;
using BugTrackerAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<TicketsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TicketsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TicketsController>
        [HttpPost]
        public async Task<ActionResult<TicketDto>> Post([FromBody] NewTicketDto newTicketDto)
        {
            var project = _unitOfWork.Projects
                .Find(p => p.Name.ToLower() == newTicketDto.ProjectName.ToLower())
                .SingleOrDefault();
            if (project == null) return BadRequest("Project Not Found");

            var ticket = new Ticket
            {
                Title = newTicketDto.Title,
                Description = newTicketDto.Description,
                Priority = newTicketDto.Priority,
                Type = newTicketDto.Type,
                Status = "New",
                UserId = newTicketDto.CreatedByUserId,
                ProjectId = project.Id
            };
            _unitOfWork.Tickets.Add(ticket);
            var result = await _unitOfWork.SaveChangesAsync();

            var projectTicket = new ProjectTicket { ProjectId = project.Id, TicketId = ticket.Id };
            _unitOfWork.ProjectTickets.Add(projectTicket);
            var projectTicketResult = await _unitOfWork.SaveChangesAsync();

            if (result == true && projectTicketResult == true) {
                var ticketDto = _mapper.Map<TicketDto>(ticket);
                return Ok(ticketDto);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // PUT api/<TicketsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
