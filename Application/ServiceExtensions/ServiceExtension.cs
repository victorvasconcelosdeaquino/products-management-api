using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Mappings;
using Domain.Services;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.Interfaces.IServices;
using Project.Core.Services;

namespace rental_movie_api.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //Registering the dependence injection to services
            #region Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
            #endregion  Services

            //Registering the dependence injection to repositories
            #region Repositories
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            #endregion Repositories

            //Registering the dependence injection to AutoMapper
            #region Mapper
            var mapping = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapping.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            return services;
        }
    }
}
