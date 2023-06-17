using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Onion.Api.Application.Interfaces.Repositories;
using Onion.Api.Domain.Models;
using Onion.Common.Infrastructure;
using Onion.Common.Infrastructure.Extensions;
using Onion.Common.Models.Queries;
using Onion.Common.Models.RequestedModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Onion.Api.Application.Features.Commands.User.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            if (dbUser == null)
                throw new DataBaseValidationException("Kullanıcı Bulunamadı!");

            var pass = PasswordEncryptor.Encrpt(request.Password);
            if (dbUser.Password != pass)
                throw new DataBaseValidationException("Kullanıcı adı yada şifre Yanlış!");

            if (!dbUser.EmailConfirmed)
                throw new DataBaseValidationException("Email henüz doğrulanmadı!");

            var result = _mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbUser.EmailAddress),
            new Claim(ClaimTypes.Name, dbUser.UserName),
            new Claim(ClaimTypes.GivenName, dbUser.FirstName),
            new Claim(ClaimTypes.Surname, dbUser.LastName)
            };

            result.Token = GenerateToken(claims);

            return result;
        }
        private string GenerateToken(Claim[] claims)
        {
            try
            {

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:Secret"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddDays(10);

                var token = new JwtSecurityToken(claims: claims,
                                                 expires: expiry,
                                                 signingCredentials: creds,
                                                 notBefore: DateTime.Now);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                var den1 = ex.InnerException;
                var den2 = ex.Message;


                var claimss = new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, ""),
            new Claim(ClaimTypes.Email, ""),
            new Claim(ClaimTypes.Name, ""),
            new Claim(ClaimTypes.GivenName, ""),
            new Claim(ClaimTypes.Surname, "")
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:Secret"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(claims: claims,
                                                 expires: DateTime.Now.AddDays(2),
                                                 signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                                                 notBefore: DateTime.Now);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
        }
    }
}
