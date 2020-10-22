using Microsoft.EntityFrameworkCore;
using capgemini_api.Models.Entitys;

namespace capgemini_api.Data.DataContext
{
  public class ApiContext : DbContext
  {
    public DbSet<Importacao> Importacoes { get; set; }

    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
  }
}