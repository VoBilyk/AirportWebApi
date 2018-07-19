using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using Airport.DAL;
using Airport.DAL.Interfaces;
using Airport.DAL.Entities;
using Airport.BLL.Interfaces;
using Airport.BLL.Services;
using Airport.Shared.DTO;
using Airport.BLL.Validators;
using Airport.BLL.Mappers;

namespace Airport.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Instance injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IAeroplaneService, AeroplaneService>();
            services.AddScoped<IAeroplaneTypeService, AeroplaneTypeService>();
            services.AddScoped<ICrewService, CrewService>();
            services.AddScoped<IPilotService, PilotService>();
            services.AddScoped<IStewardessService, StewardessService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IDepartureService, DepartureService>();
            
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralMapperProfile()));
            services.AddSingleton(_ => config.CreateMapper());

            services.AddTransient<IValidator<Aeroplane>, AeroplaneValidator>();
            services.AddTransient<IValidator<AeroplaneType>, AeroplaneTypeValidator>();
            services.AddTransient<IValidator<Flight>, FlightValidator>();
            services.AddTransient<IValidator<Pilot>, PilotValidator>();
            services.AddTransient<IValidator<Stewardess>, StewardessValidator>();
            services.AddTransient<IValidator<Crew>, CrewValidator>();
            services.AddTransient<IValidator<Departure>, DepartureValidator>();
            services.AddTransient<IValidator<Ticket>, TicketValidator>();

            services.AddDbContext<AirportContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("AirportDb")),//, b => b.MigrationsAssembly("Airport.DAL")),
               ServiceLifetime.Singleton);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
