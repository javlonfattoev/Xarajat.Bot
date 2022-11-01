using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xarajat.Data.Entities;

namespace Xarajat.Data.Context;

#pragma warning disable CS8618
[Scoped]
public class XarajatDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Outlay> Outlays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=sql.bsite.net\MSSQL2016;Database=fattoev2_xarajatbot;User Id=fattoev2_xarajatbot;Password=xarajatbot13;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //with configuration class
        OutLaysConfiguration.Configure(modelBuilder.Entity<Outlay>());

        //with configuration classes from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(XarajatDbContext).Assembly);
    }
}