using Onion.Api.Application.Interfaces.Repositories;
using Onion.Api.Domain.Models;
using Onion.Infrastructure.Persistence.Context;

namespace Onion.Infrastructure.Persistence.Repositories;

public class EntryRepository : GenericRepository<Entry>, IEntryRepository
{
    protected EntryRepository(OnionContext dbContext) : base(dbContext)
    {
    }
}