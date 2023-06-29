using Microsoft.EntityFrameworkCore;

namespace final.Models
{
    public class CarsContext: DbContext
    {
        public CarsContext(DbContextOptions<CarsContext> options) : base(options) { }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
    }
}
