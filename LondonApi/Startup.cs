using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using LondonApi.Infractures;
using Microsoft.AspNetCore.Mvc;
using LondonApi.Filters;
using LondonApi.Models;
using LondonApi.Services;
using AutoMapper;

namespace LondonApi
{
    public class Startup
    {
        private readonly int? _httpsPort;
        private readonly IHostingEnvironment _env;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;

            // Get the Https port only in development environment
            if (_env.IsDevelopment())
            {
                var launchJsonConfig = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("Properties\\launchSettings.json")
                    .Build();
                _httpsPort = launchJsonConfig.GetValue<int>("iisSettings:iisExpress:sslPort");
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Use an in-memory database for quick dev and testing
            // TODO: Swap out with a real database in production
            services.AddDbContext<HotelApiContext>(opt => opt.UseInMemoryDatabase());

            services.AddAutoMapper();

            services.AddMvc(opt=>
            {
                opt.Filters.Add(typeof(JsonExceptionFilter));

                // Require HTTPS for all controllers
                opt.Filters.Add(typeof(RequireHttpsAttribute));
                opt.SslPort = _httpsPort;

                var jsonFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter>().Single();
                opt.OutputFormatters.Remove(jsonFormatter);
                opt.OutputFormatters.Add(new IonOutputFormatter(jsonFormatter));
            });

            services.AddRouting(opt => opt.LowercaseUrls = true);

            services.AddApiVersioning(opt =>
            {
                opt.ApiVersionReader = new MediaTypeApiVersionReader();
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new ApiVersion(1,0);
                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt);
            });

            services.Configure<HotelInfo>(Configuration.GetSection("Info"));

            services.AddScoped<IRoomService, DefaultRoomService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<HotelApiContext>();
                    AddTestData(context);
                }
                    
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts(opt =>
            {
                opt.MaxAge(days: 180);
                opt.IncludeSubdomains();
                opt.Preload();
            });

            app.UseMvc();
        }

        private static void AddTestData(HotelApiContext context)
        {
            context.Rooms.Add(new RoomEntity
            {
                Id = new Guid("703761e1-b729-47cf-a62a-9f21d99d0da9"),
                Name="Oxford Suite",
                Rate=10119
            });
            context.Rooms.Add(new RoomEntity
            {
                Id= new Guid("90f31833-fa92-4418-ad73-78a031619b79"),
                Name="Driscoll suite",
                Rate=23959
            });
            context.SaveChanges();
        }
    }
}
