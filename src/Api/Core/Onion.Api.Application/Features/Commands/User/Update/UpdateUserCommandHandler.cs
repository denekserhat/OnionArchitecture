using AutoMapper;
using MediatR;
using Onion.Api.Application.Interfaces.Repositories;
using Onion.Common.Infrastructure.Extensions;
using Onion.Common.Models.RequestedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Api.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(request.Id);

            if (dbUser is not null)
                throw new DataBaseValidationException("User already exists!");

            _mapper.Map(request, dbUser);
            var rows = await _userRepository.UpdateAsync(dbUser);



        }
    }
}
