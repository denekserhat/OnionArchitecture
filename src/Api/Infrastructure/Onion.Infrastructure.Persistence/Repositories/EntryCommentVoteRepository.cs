using Onion.Api.Application.Interfaces.Repositories;
using Onion.Api.Domain.Models;
using Onion.Infrastructure.Persistence.Context;

namespace Onion.Infrastructure.Persistence.Repositories;

public class EntryCommentVoteRepository : GenericRepository<EntryCommentVote>, IEntryCommentVoteRepository
{
    protected EntryCommentVoteRepository(OnionContext dbContext) : base(dbContext)
    {
    }
}