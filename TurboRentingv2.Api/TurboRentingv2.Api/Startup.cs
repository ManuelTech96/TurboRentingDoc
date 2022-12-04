using System.Reflection;
using TurboRentingv2.Api.Context;
using TurboRentingv2.Api.Interfaces;
using TurboRentingv2.Api.Interfaces.Repositories;
using TurboRentingv2.Api.Interfaces.Services;
using TurboRentingv2.Api.Mapping;
using TurboRentingv2.Api.Models.Repositories;
using TurboRentingv2.Api.Services;

namespace TurboRentingv2.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TurboRentingContext>();

            services.AddScoped<IMapper, Mapper>();

            Assembly? profilesAssembly = Assembly.GetAssembly(typeof(Mapper));

            services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
            {
                var profiles = profilesAssembly?.GetTypes().Where(x => typeof(AutoMapper.Profile).IsAssignableFrom(x));
                AddProfilesToAutomapper(cfg, profiles);

                profiles = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(AutoMapper.Profile).IsAssignableFrom(x));
                AddProfilesToAutomapper(cfg, profiles);

            }).CreateMapper());

            

            /*Repositories*/
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IGarageRepository, GarageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IVehiculeRepository, VehiculeRepository>();

            /*BussinessServices*/
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IGarageService, GarageService>();
            services.AddScoped<IUserService, UserService>();
           //services.AddScoped<IVehiculeService, VehiculeService>();

            services.AddSwaggerGen();
            services.AddMvc();

            services.AddControllers().AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddProfilesToAutomapper(AutoMapper.IMapperConfigurationExpression cfg, IEnumerable<Type>? profiles)
        {
            if (profiles != null)
            {
                foreach (var profile in profiles)
                    cfg.AddProfile(profile);
            }
        }
    }
}
