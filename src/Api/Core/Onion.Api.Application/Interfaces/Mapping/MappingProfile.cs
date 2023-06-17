using AutoMapper;
using Onion.Api.Domain.Models;
using Onion.Common.Models.Queries;
//using Onion.Common.Models.RequestModels;

namespace Onion.Api.Application.Interfaces.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>()
            .ReverseMap();

        //CreateMap<CreateUserCommand, User>();

        //CreateMap<UpdateUserCommand, User>();

        //CreateMap<CreateEntryCommand, Entry>()
        //    .ReverseMap();

        //CreateMap<CreateEntryCommentCommand, EntryComment>()
        //    .ReverseMap();
    }
}