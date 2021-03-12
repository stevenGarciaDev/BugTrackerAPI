using AutoMapper;
using BugTrackerAPI.DataTransferObjects;
using BugTrackerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BugTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("member/{username}")]
        public ActionResult<ProjectMemberDto> GetByUsername(string username)
        {
            var user = _unitOfWork.Users
                .Find(u => u.UserName.ToLower() == username.ToLower())
                .SingleOrDefault();

            if (user == null)
            {
                return BadRequest("Username not found");
            }

            var projectMemberDto = _mapper.Map<ProjectMemberDto>(user);
            return Ok(projectMemberDto);
        }
    }
}
