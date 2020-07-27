using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TG.Domain.Interfaces.Hub;
using TG.Infrastructure;
using TG.Infrastructure.AutoMapper;
using TG.Infrastructure.EF;
using TG.Infrastructure.Hub;

namespace Gateway
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
			services.AddCors(); 
			#region Db
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly("TG.Infrastructure")));
			services.AddScoped<UnitOfWork>();
			#endregion
			#region MediatR
			services.AddMediatR(AppDomain.CurrentDomain.Load("TG.Application"));
			#endregion

			services.AddAutoMapper(typeof(AutoMapping));

			services.AddControllers()
				.AddNewtonsoftJson(options =>
				    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

			services.AddSignalR();
			services.AddSingleton<IGameRepository>(new GameRepository());

			#region Swagger
			services.AddSwaggerGen(c =>
			{
			
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "TG.WebApi",
				});

			});
			#endregion

			//TO DO
			//services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
			//services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

			services.AddControllers().AddNewtonsoftJson(options =>
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
); 
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			//app.UseAuthorization();

			#region Swagger
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "TG.WebApi");
			});
			#endregion


			app.UseCors(builder => builder
	.AllowAnyHeader()
	.AllowAnyMethod()
	.SetIsOriginAllowed((host) => true)
	.AllowCredentials()
  );

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<GameHub>("/trivia");
			});
		}
	}
}
