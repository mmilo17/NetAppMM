using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetApp.API.Data;
using NetApp.API.Dtos;
using NetApp.API.Helpers;
using NetApp.API.Models;

namespace NetApp.API.Controllers
{
    [ServiceFilter(typeof(LoginUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IFriendRepository _repo;

        private readonly IMapper _mapper;

        public UsersController(IFriendRepository repo, IMapper mapper)
        {
            _mapper =mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            // if (string.IsNullOrEmpty(userParams.Gender))
            // {
            //     userParams.Gender = userFromRepo.Gender == "male" ? "female" : "male";
            // }

            var users = await _repo.GetUsers(userParams);

            var userToReturn = _mapper.Map<IEnumerable<UserFroListDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(userToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpPost("{id}/Request/{recipientId}")]
        public async Task<IActionResult> RequestUser(int id, int recipientId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var request = await _repo.GetRequest(id, recipientId);

            if (request != null)
                return BadRequest("You already send request to this user");   

            if (await _repo.GetUser(recipientId) == null)
                return NotFound();    

            request = new Request
            {
                SenderId = id,
                ReceiverId = recipientId
            };

            _repo.Add<Request>(request);

            if (await _repo.SaveAll())
                return Ok();   

            return BadRequest("Failed to Send friend request");     
        }
    }
}