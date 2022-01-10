using API.Helpers;
using API.Models.Data;
using API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
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
            services.AddCors(option =>
            {
                option.AddPolicy("NubimetricsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nubimetrics API", Version = "Versión 1" });
            });

            services.AddAutoMapper(c => c.AddProfile<AutoMappingHelper>(), typeof(Startup));

            services.AddDbContext<NubimetricsExampleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("NubimetricsDatabase")
            ));
            services.AddScoped(typeof(IEFRepository<>), typeof(EFRepository<>));

            services.AddSingleton<IPasswordHelper, PasswordHelper>();
            services.AddSingleton<ILogHelper, LogHelper>();
            services.AddHttpClient<IHttpHelper, HttpHelper>("HttpHelper");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Versión 1"));
            }

            app.UseCors("NubimetricsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
