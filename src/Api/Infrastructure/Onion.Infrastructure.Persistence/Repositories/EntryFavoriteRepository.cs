using Onion.Api.Application.Interfaces.Repositories;
using Onion.Api.Domain.Models;
using Onion.Infrastructure.Persistence.Context;

namespace Onion.Infrastructure.Persistence.Repositories;

public class EntryFavoriteRepository : GenericRepository<EntryFavorite>, IEntryFavoriteRepository
{
    protected EntryFavoriteRepository(OnionContext dbContext) : base(dbContext)
    {
    }
}