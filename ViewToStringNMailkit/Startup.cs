using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ViewToStringNMailKit.Services.ViewRender;

namespace ViewToStringNMailKit
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
            //Register ViewRenderService as a Singlton
            services.AddSingleton<IViewRendererService, ViewRendererService>();

            //Register Swagger generator
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "GP API", Version = "v1" }));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            //Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying JSON end point.
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GP API"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSwagger();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapSwagger();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
