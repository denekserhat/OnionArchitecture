using Onion.Api.Application.Interfaces.Repositories;
using Onion.Api.Domain.Models;
using Onion.Infrastructure.Persistence.Context;

namespace Onion.Infrastructure.Persistence.Repositories;

public class EmailConfirmationRepository : GenericRepository<EmailConfirmation>, IEmailConfirmationRepository
{
    protected EmailConfirmationRepository(OnionContext dbContext) : base(dbContext)
    {
    }
}