using System.Threading;
using System.Threading.Tasks;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace MigrationWorker
{
    public class Worker : IHostedService
    {
        private readonly ApplicationDataContext _context;

        public  Worker(ApplicationDataContext context)
        {
            _context = context;
        }
        
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _context.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}