using System.Linq;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Database
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
            
        }

        public  DbSet<Kittens> Kittens { get; set; }
        public  DbSet<Persons> Persons { get; set; }
        
        public  DbSet<Clinic> Clinics { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}