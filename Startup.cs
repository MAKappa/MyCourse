using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Applications;
using MyCourse.Models.Services.Infrastructure;

namespace MyCourse
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddTransient<ICourseService, AdoNetCourseService>();
            //services.AddTransient<IDatabaseAccesser, SqliteDatabaseAccesser>();
            services.AddTransient<ICourseService,EFCoreCourseService>();
            //services.AddDbContext<MyCourseDbContext>();
            
            services.AddDbContextPool<MyCourseDbContext>(OptionsBuilder=>{
                string connectionString= Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                OptionsBuilder.UseSqlite(connectionString);
            });
            

            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<CoursesOptions>(Configuration.GetSection("Courses"));
       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
            }

           
            //app.UseMvcWithDefaultRoute();
            
            app.UseMvc(routeBuilder  =>
            {
                routeBuilder.MapRoute("default=Home","{controller=Home}/{action=Index}/{id?}");
               
            });
        }

        
    }
}
