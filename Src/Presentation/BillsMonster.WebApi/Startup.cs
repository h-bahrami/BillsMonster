using AutoMapper;
using BillsMonster.Application.Bills.Queries.List;
using BillsMonster.Application.Infrastructure;
using BillsMonster.Application.Infrastructure.AutoMapper;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Infrastructure;
using BillsMonster.Infrastructure;
using BillsMonster.Persistence;
using BillsMonster.WebApi.Filters;
using MediatR.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace BillsMonster.WebApi
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
            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                    options.EnableEndpointRouting = false;
                })
                // .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson();
            //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>());

            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            #region MediatR

            services.AddScoped<ServiceFactory>(p => p.GetService);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //services.AddMediatR(typeof(GetBillsListQuery).GetTypeInfo().Assembly);
            services.Scan(scan =>
            scan.FromAssembliesOf(typeof(IMediator), typeof(GetBillsListQuery))
            .AddClasses()
            .AsImplementedInterfaces());

            #endregion

            #region DB Connection
            services.Configure<MongodbConnection>(Configuration.GetSection(nameof(MongodbConnection)));
            services.AddSingleton<IMongodbConnection>(sp => sp.GetRequiredService<IOptions<MongodbConnection>>().Value);
            services.AddScoped<IBillsMonsterDbContext, BillsMonsterDbContext>();
            #endregion

            services.AddTransient<IBillsRepository, BillsRepository>();

            services.AddTransient<INotificationService, NotificationService>();
            //services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            //app.UseRouting(routes =>
            //{
            //    routes.MapControllers();
            //});

            //app.UseSwagger();
            //app.UseSwaggerUi3();
            app.UseAuthorization();
            app.UseMvc();
        }
    }
}
