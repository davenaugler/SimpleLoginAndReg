using Microsoft.EntityFrameworkCore;

namespace SimpleLoginAndReg.Models
{
  public class SimpleLoginAndRegContext : DbContext
  {
    public SimpleLoginAndRegContext(DbContextOptions options) : base(options){}
    public DbSet<RegUser> Users {get; set;}
  }
}