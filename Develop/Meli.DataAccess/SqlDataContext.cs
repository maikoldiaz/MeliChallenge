namespace Meli.DataAccess;

using System.Threading;
using System.Threading.Tasks;
using Meli.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Meli.Entities;
public class SqlDataContext : DbContext, ISqlDataContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MeliDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity => {
            entity.Property(p => p.Price);
            entity.Property(p => p.BasePrice);
        });
    }

    /// <summary>
    /// Clears this instance.
    /// </summary>
    public void Clear()
    {
        var clearables = this.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted).ToList();

        clearables.ForEach(x => x.State = EntityState.Detached);
    }

    /// <summary>
    /// Saves the context asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// Number of rows effected.
    /// </returns>
    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        try
        {

            var count = await this.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }
        catch
        {
            this.Clear();
            throw;
        }
    }

    public DbSet<Product>? Products { get; set; }


}