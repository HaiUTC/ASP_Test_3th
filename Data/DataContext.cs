using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebNet.Models;
namespace WebNet.Data{
    public class DataContext: DbContext, IDataContext{
        public DataContext(){}

        public DataContext(DbContextOptions<DataContext> options): base(options){}
        public DbSet<User> Users { get; set;}

        public Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}