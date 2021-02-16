using EmployeesAndCompanies.Domain.Interfaces;
using EmployeesAndCompanies.Persistence;
using EmployeesAndCompanies.Service.Interfaces;
using EmployeesAndCompanies.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeesAndCompanies.Application
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env) =>
            (Configuration, Env) = (configuration, env);

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = DatabaseConnectionString;
            services.AddSingleton(_ => Configuration);
            services.AddTransient<IEmployeeRepository, EmployeeRepository>(_ => new EmployeeRepository(connectionString));
            services.AddTransient<IBusinessEntityRepository, BusinessEntityRepository>(_ => new BusinessEntityRepository(connectionString));
            services.AddTransient<ICompanyRepository, CompanyRepository>(_ => new CompanyRepository(connectionString));

            services.AddScoped<IBusinessEntityService, BusinessEntityService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
        
        private string DatabaseConnectionString =>
            Env.IsDevelopment()
                // ? Configuration["DbConnection"]
                ? Configuration.GetConnectionString("Local")
                : "todo: add connection string for prod";
    }
}