using System.Collections.Generic;
using System.Threading.Tasks;
using WebNet.Models;

namespace WebNet.Repositories
{
    public interface IUserRepository
    {
         Task<User> Get(int id);
         Task<IEnumerable<User>> GetAll();
         Task Add(User user);
         Task Delete(int id);
         Task Update(User user);
    }
}