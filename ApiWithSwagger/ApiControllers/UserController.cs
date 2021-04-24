using ApiWithSwagger.Data;
using ApiWithSwagger.Data.Models;
using ApiWithSwagger.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithSwagger.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UserController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get User by userId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _repo.FindById(id);
            if (user != null)
            {
                var userToReturn = _mapper.Map<UserDto>(user);
                return Ok(userToReturn);
            }

            return Ok(new UserDto());
        }

        /// <summary>
        /// Get list of users, input is an array of user emails
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetListUser([FromBody] List<string> emails)
        {
            var users = await _repo.GetManyUsers(emails);
            var usersToReturn = _mapper.Map<List<UserDto>>(users);

            return Ok(usersToReturn);
        }

        /// <summary>
        /// Get an user with Deposit items
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetUserWithItems([FromBody]string email)
        {
            var user = await _repo.GetUserWithItems(email);
            var userToReturn = _mapper.Map<UserDto>(user);

            return Ok(userToReturn);
        }

        /// <summary>
        /// Add an User
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser([FromBody] UserDto input)
        {
            var user = await _repo.GetUser(input.Email);

            if (user != null)
                return Ok("User already exist");

            var userToAdd= _mapper.Map<User>(input);
            _repo.Add(userToAdd);
            if(await _repo.SaveAll())
                return Ok();

            return BadRequest("Operation Failed!");
        }

        /// <summary>
        /// Update an User
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto input)
        {
            var user = await _repo.GetUser(input.Email);
            if (user != null)
            {
                _mapper.Map(input, user);
                _repo.Update(user);
                if (await _repo.SaveAll())
                    return Ok();
                else
                    return BadRequest("Operation Failed");
            }

            return BadRequest("User Not Found");
        }
    }
}
