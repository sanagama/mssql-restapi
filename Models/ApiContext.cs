using Microsoft.EntityFrameworkCore;

namespace MSSqlWebapi.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Database> Databases { get; set; }
    }  
}