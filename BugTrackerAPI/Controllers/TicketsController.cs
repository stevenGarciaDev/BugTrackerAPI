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

        // GET api/<TicketsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> Get(int id)
        {
            var ticket = await _unitOfWork.Tickets.FindByIdAsync(id);
            var ticketDto = _mapper.Map<TicketDto>(ticket);
            return Ok(ticketDto);
        }

        // GET api/<TicketsController>/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsForAUser(int userId)
        {
            var tickets = await _unitOfWork.Tickets.GetAllTicketsForAUser(userId);
            return Ok(tickets);
        }

        // GET api/<TicketsController>project//5
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsForProject(int projectId)
        {
            var tickets = await _unitOfWork.Tickets.GetTicketsInAProject(projectId);

            return Ok(tickets);
        }

        [HttpGet("all-projects/{userId}")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAllTicketsForUserProjects(int userId)
        {
            var projects = await _unitOfWork.ProjectMembers.GetProjectsForUser(userId);
            var allTickets = await _unitOfWork.ProjectTickets.GetAllTicketsForProjects(projects);

            return Ok(allTickets);
        }

        // GET api/<TicketsController>assigned/5
        [HttpGet("assigned/{ticketId}")]
        public ActionResult<TicketDto> GetAssignedUserForTicket(int ticketId)
        {
            var tickets = _unitOfWork.Tickets.GetTicketWithAssignedUser(ticketId);

            return Ok(tickets);
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

            if (result == true && projectTicketResult == true)
            {
                var ticketDto = _mapper.Map<TicketDto>(ticket);
                return Ok(ticketDto);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // PUT api/<TicketsController>/5
        [HttpPut("{ticketId}")]
        public async Task<ActionResult<TicketDto>> Put(int ticketId, [FromBody] TicketDto updatedTicket)
        {
            var ticket = await _unitOfWork.Tickets.FindByIdAsync(ticketId);

            ticket.Type = updatedTicket.Type;
            ticket.Priority = updatedTicket.Priority;
            ticket.Status = updatedTicket.Status;
            ticket.UserId = updatedTicket.UserId;

            var result = await _unitOfWork.SaveChangesAsync();

            if (result == true)
            {
                var updatedTicketDto = _mapper.Map<TicketDto>(ticket);
                return Ok(updatedTicketDto);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
