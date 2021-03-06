using Core.Converters;
using Core.DTOs;
using Core.Entities;
using Core.Entities.Identity;
using Core.Helper;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Identity;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Exceptions;

namespace Web
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
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CatalogConnection")));

            services.AddDbContext<IdentityContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(
                  opts =>
                  {
                      opts.Password.RequiredLength = 6;
                      opts.Password.RequireNonAlphanumeric = false;
                      opts.Password.RequireLowercase = false;
                      opts.Password.RequireUppercase = false;
                      opts.Password.RequireDigit = false;
                  })
                  .AddEntityFrameworkStores<IdentityContext>();


            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            services.AddTransient<IStatusEquipmentService, StatusEquipmentService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<IEquipmentTypeService, EquipmentTypeService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IConverter<Employee, EmployeeDTO>, EmployeeConverter>();
            services.AddTransient<IConverter<Equipment, EquipmentDTO>, EquipmentConverter>();

            services.AddTransient<IUserHelper, UserHelper>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.EnvironmentName = "Production";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //    app.UseHsts();
            }

            app.UseExceptionHandler("/Home/Error");
            app.UseStatusCodePagesWithReExecute("/Home/Error", "?requestId={0}");

            app.UseMiddleware<ExceptionInterceptor>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
