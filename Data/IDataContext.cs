using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebNet.Models;

namespace WebNet.Data{
    public interface IDataContext{
        DbSet<User> Users { get; set;}
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}