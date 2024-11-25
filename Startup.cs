using EpicorRestAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace EPAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "SAI API", Version = "v2" });
            });
            services.AddCors(options =>
             options.AddPolicy("AllowAllOrigins",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader();
                        })
                );

            services.AddControllers();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAllOrigins");
           
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
                    
                });
            EpicorRest.AppPoolHost = Configuration["AppPoolHost"]; //TLD to your Epicor Server
            EpicorRest.AppPoolInstance = Configuration["AppPoolInstance"]; ; //Epicor AppServer Insdtance
            EpicorRest.APIKey = Configuration["Key"]; //API key required with V2
            EpicorRest.Company = Configuration["Company"]; //Epicor Company (Current company) Required with V2
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
