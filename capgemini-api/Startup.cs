using capgemini_api.Business;
using capgemini_api.Data;
using capgemini_api.Data.DataContext;
using capgemini_api.Services;
using capgemini_api.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace capgemini_api
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
            services.AddControllers();

            //configurando Entity para a base de dados de trabalho
            services.AddDbContext<ApiContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("bdimportacoes"));
                }
            );

            services.AddCors(options => 
            options.AddPolicy("AllowCredentials", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddScoped<ImportacaoBusiness>();
            services.AddScoped<ImportacaoData>();

            services.AddScoped<IMultipartFormDataService, MultipartFormDataService>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

             app.UseCors(a => {
                a.AllowAnyOrigin();
                a.AllowAnyHeader();
                a.AllowAnyMethod();
            });
      
            app.UseCors("AllowCredentials");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
