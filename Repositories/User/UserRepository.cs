using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebNet.Data;
using WebNet.Models;
using Microsoft.EntityFrameworkCore;
namespace WebNet.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataContext _context;
        public UserRepository(IDataContext context){
            _context = context;
        }
        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangeAsync();
        }

        public async Task Delete(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if(userToDelete == null){
                throw new NullReferenceException();
            }
            _context.Users.Remove(userToDelete);
            await _context.SaveChangeAsync();
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users.FindAsync(id);
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
            await _context.SaveChangeAsync();

        }
    }
}