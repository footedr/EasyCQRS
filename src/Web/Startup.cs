using System;
using System.Linq;
using EasyCQRS.Web.Data;
using EasyCQRS.Web.Features.Employees;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace EasyCQRS.Web
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", false, true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			// register MediatR types in DI container
			services.AddMediatR(typeof(Startup).Assembly);
			
			// hire employee handlers
			services.AddScoped<IRequestHandler<HireEmployeeCommand, HireEmployeeCommandResponse>, HireEmployeeCommandHandler>();

			// employee query handlers
			services.AddScoped<IRequestHandler<EmployeeListQuery, EmployeeListQueryResponse>, EmployeeListQueryHandler>();
			services.AddScoped<IRequestHandler<EmployeeIdQuery, EmployeeIdQueryResponse>, EmployeeIdQueryHandler>();

			// edit employee
			/*
			services.AddScoped<IPipelineBehavior<EditEmployeeCommand, Unit>, EditEmployeeValidationBehavior>();
			services.AddScoped<IRequestHandler<EditEmployeeCommand>, EditEmployeeCommandHandler>();
			*/

			// terminate employee
			services.AddScoped<IRequestHandler<TerminateEmployeeCommand>, TerminateEmployeeCommandHandler>();

			// register domain services
			services.AddScoped<IEmployeeDomain, EmployeeDomain>();

			// register IOptions
			services.AddOptions();

			// configure connection strings
			services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
			var connectionStrings = new ConnectionStrings
			{
				EasyCqrsDb = Configuration.GetConnectionString("EasyCqrsDb")
			};

			// register dbcontext
			// services.AddDbContext<EasyCqrsContext>(options => { options.UseInMemoryDatabase("EasyCqrsDb"); }, ServiceLifetime.Singleton);
			services.AddDbContext<EasyCqrsContext>(
				options => options.UseSqlServer(Options.Create(connectionStrings).Value.EasyCqrsDb)
			);

			// configure view locations
			services.Configure<RazorViewEngineOptions>(options =>
			{
				options.ViewLocationExpanders.Add(new FeatureLocationExpander());
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					"default",
					"{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}