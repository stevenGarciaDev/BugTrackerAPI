using AutoMapper;
using BugTrackerAPI.Data;
using BugTrackerAPI.DataTransferObjects;
using BugTrackerAPI.Entities;
using BugTrackerAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BugTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<ProjectsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> Get()
        {
            var allProjects = await _unitOfWork.Projects.GetAllAsync();
            return Ok(allProjects);
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{projectId}")]
        public async Task<ActionResult<IEnumerable<Project>>> Get(int projectId)
        {
            var project = await _unitOfWork.Projects.GetAsync(projectId);
            return Ok(project);
        }

        // POST api/<ProjectsController>
        [Authorize(Roles = "Project Manager")]
        [HttpPost]
        public async Task<ActionResult<Project>> Post([FromBody] ProjectDto projectDto)
        {
            var projectName = projectDto.Name.ToLower();
            var existingProject = _unitOfWork.Projects.Find(p => p.Name.ToLower() == projectName);
            if (existingProject.Any()) return BadRequest("That Project Name is already taken.");
            
            var project = _mapper.Map<Project>(projectDto);

            _unitOfWork.Projects.Add(project);

            // add to ProjectMember, add project id and user id 
            var result = await _unitOfWork.SaveChangesAsync();

            if (result == true) 
            {
                return Ok(project);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{projectId}")]
        public Task<ActionResult<Project>> Put(int projectId, [FromBody] ProjectDto projectDto)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{projectId}")]
        public async Task<ActionResult> Delete(int projectId)
        {
            var project = await _unitOfWork.Projects.FindByIdAsync(projectId);
            _unitOfWork.Projects.Remove(project);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
