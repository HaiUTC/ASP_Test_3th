using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebNet.Data;
using WebNet.Models;
using Microsoft.EntityFrameworkCore;
using Utils.CustomException;
using WebNet.Dtos.User;
using Utils.Utilities;
using System.Security.Cryptography;
using System.Text;

namespace WebNet.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context){
            _context = context;
        }
        public async Task<AuthenticatedUser> Register(UserRegisterDto userRegisterDto)
        {
            var isEmailExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == userRegisterDto.Email);
            if(isEmailExists != null){
                throw new EmailAlreadyExistsException("Email already exists");
            }
            if(!string.IsNullOrEmpty(userRegisterDto.Password)){
                userRegisterDto.Password = GetMD5(userRegisterDto.Password);
            }
            var Username = userRegisterDto.LastName + " " + userRegisterDto.FirstName;
            User user = new(){
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Gender = userRegisterDto.Gender,
                Email = userRegisterDto.Email,
                Password = userRegisterDto.Password,
                CreatedAt = DateTime.Now
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new AuthenticatedUser{
                Username = Username,
                Token = JwtGenerator.GenerateAuthToken(Username)
            };
        }
        public async Task<AuthenticatedUser> Login(UserLoginDto userLogin)
        {
            var userAuthenticated = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLogin.Email);
            if(userAuthenticated == null || userAuthenticated.Password == null || !userAuthenticated.Password.Equals(GetMD5(userLogin.Password))){
                 throw new InvalidEmailPasswordException("Invalid email or password");
            }
            var Username = userAuthenticated.LastName + " " + userAuthenticated.FirstName; 
            return new AuthenticatedUser{
                Username = Username,
                Token = JwtGenerator.GenerateAuthToken(Username)
            };
        }

        public async Task Delete(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if(userToDelete == null){
                throw new NullReferenceException();
            }
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Update(User user)
        {
            var userToUpdate = await _context.Users.FindAsync(user.UserId);
            if(userToUpdate == null){
                throw new NullReferenceException();
            }
            userToUpdate = user;
            await _context.SaveChangesAsync();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
 
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
                
            }
            return byte2String;
        }
    }
}