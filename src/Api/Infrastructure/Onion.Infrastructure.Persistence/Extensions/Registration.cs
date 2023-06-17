using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onion.Api.Application.Interfaces.Repositories;
using Onion.Api.Domain.Models;
using Onion.Infrastructure.Persistence.Context;
using Onion.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OnionContext>(conf =>
            {
                var connStr = configuration.GetConnectionString("OnionMsSQL");
                conf.UseSqlServer(connStr, opt =>
                {
                    //db ye bağlanırken hata yaşanırsa
                    opt.EnableRetryOnFailure();
                });
            });

            services.AddScoped<IUserRepository, UserRepository>();

            //temp data oluşturmak için
            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

            return services;
        }
    }
}
