using System.Collections.Generic;
using System.Threading.Tasks;
using WebNet.Dtos.User;
using WebNet.Models;

namespace WebNet.Repositories
{
    public interface IUserRepository
    {
         Task<AuthenticatedUser> Login(UserLoginDto userLogin);
         Task<IEnumerable<User>> GetAll();
         Task<AuthenticatedUser> Register(UserRegisterDto userRegisterDto);
         Task Delete(int id);
         Task Update(User user);
    }
}