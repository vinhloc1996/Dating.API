using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Dating.API.Data;
using Dating.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dating.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(userToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDto userForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await _repo.GetUser(id);

            if (userFromRepo == null)
            {
                return NotFound($"Could not find user with an ID of {id}");
            }

            if (currentUser != userFromRepo.Id)
            {
                return Unauthorized();
            }

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            
            throw new Exception($"Updating user {id} failed on save");
        }
    }
}