using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Utils.Utilities;
using WebNet.Data;
using WebNet.Dtos.User;
using WebNet.Models;
using WebNet.Repositories;
using Microsoft.EntityFrameworkCore;
using Utils.CustomException;

namespace WebNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository){
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (UserLoginDto userLogin){
            try
            {
                var result = await _userRepository.Login(userLogin);
                return Created("", result);
            }
            catch(InvalidEmailPasswordException e)
            {
                return StatusCode(401, e.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers () {
            var user = await _userRepository.GetAll();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto){
            try
            {
                var result = await _userRepository.Register(userRegisterDto);
                return Created("", result);
            }
            catch(EmailAlreadyExistsException e)
            {
                return StatusCode(409, e.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser (int id){
            try
            {
                 await _userRepository.Delete(id);
                 return Created("", null);
            }
            catch (NullReferenceException e)
            {
               return StatusCode(400, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser (int id, UserUpdateDto userUpdateDto){
            try
            {
                User user = new(){
                    UserId = id,
                    FirstName = userUpdateDto.FirstName,
                    LastName = userUpdateDto.LastName,
                    Gender = userUpdateDto.Gender,
                    Email = userUpdateDto.Email,
                    Password = userUpdateDto.Password,
                };
                await _userRepository.Update(user); 
                return Created("", null);
            }
            catch (NullReferenceException e)
            {
                return StatusCode(400, e.Message);
            }
            
            
        }
    }
}
