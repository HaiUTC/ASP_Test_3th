using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebNet.Dtos;
using WebNet.Models;
using WebNet.Repositories;

namespace WebNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository){
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser (int id){
            var user = await _userRepository.Get(id);
            if(user==null){
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers () {
            var user = await _userRepository.GetAll();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterDto userRegisterDto){
            User user = new(){
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Gender = userRegisterDto.Gender,
                Email = userRegisterDto.Email,
                Password = userRegisterDto.Password,
                CreatedAt = DateTime.Now
            };
            await _userRepository.Add(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser (int id){
            await _userRepository.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser (int id, UserUpdateDto userUpdateDto){
            User user = new(){
                UserId = id,
                FirstName = userUpdateDto.FirstName,
                LastName = userUpdateDto.LastName,
                Gender = userUpdateDto.Gender,
                Email = userUpdateDto.Email,
                Password = userUpdateDto.Password,
            };
            await _userRepository.Update(user);
            return Ok();
        }
    }
}