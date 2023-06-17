using Onion.Api.Application.Interfaces.Repositories;
using Onion.Api.Domain.Models;
using Onion.Infrastructure.Persistence.Context;

namespace Onion.Infrastructure.Persistence.Repositories;

public class EntryCommentFavoriteRepository : GenericRepository<EntryCommentFavorite>, IEntryCommentFavoriteRepository
{
    protected EntryCommentFavoriteRepository(OnionContext dbContext) : base(dbContext)
    {
    }
}