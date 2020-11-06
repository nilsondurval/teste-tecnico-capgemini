using capgemini_api.Domain.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace capgemini_api.Infra.Data.Context
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