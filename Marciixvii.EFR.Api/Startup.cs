
using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.DataAccess.Contexts;
using Marciixvii.EFR.App.Helpers.Crypt;
using Marciixvii.EFR.App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Marciixvii.EFR.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            //services.AddMvc().AddWebApiConventions();
            services.AddCors(options => {
                options.AddPolicy(Configuration["Cors:PolicyName"],
                                  builder => builder.WithOrigins(Configuration["Cors:ConfiguePolicy:Origins"])
                                                    .AllowCredentials()
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader());
            });
            services.AddDbContext<AppDbContext>(options =>
                                                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUtilisateurService, UtilisateurService>();
            services.AddScoped<ICryptography, DesCryptography>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(Configuration["Cors:PolicyName"]);

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
