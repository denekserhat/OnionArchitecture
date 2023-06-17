using Microsoft.EntityFrameworkCore;
using Onion.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Infrastructure.Persistence.Context;

public class OnionContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";

	public OnionContext()
	{
			
	}
	public OnionContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Entry> Entries { get; set; }
	public DbSet<EntryVote> EntryVotes { get; set; }
	public DbSet<EntryFavorite> EntryFavorites { get; set; }
	public DbSet<EntryComment> EntryComments { get; set; }
	public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }
	public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }
	public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		if (!optionsBuilder.IsConfigured)
		{
			var connStr = "Data Source=DESKTOP-T2QR4FL\\SQLEXPRESS;Initial Catalog=Onion;Integrated Security=True";
            optionsBuilder.UseSqlServer(connStr, opt =>
            {
                //db ye bağlanırken hata yaşanırsa
                opt.EnableRetryOnFailure();
            });
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

	public override int SaveChanges()
	{
        OnBeforeSave();
        return base.SaveChanges();
	}

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

	private void OnBeforeSave()
	{
		var addedEntities = ChangeTracker.Entries()
								.Where(x => x.State == EntityState.Added)
								.Select(x => (BaseEntity)x.Entity);
		PrepareAddedEntities(addedEntities);
	}

	private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
	{
		foreach (var entity in entities)
		{
			entity.CreateDate = DateTime.Now;
		}
	}



}
