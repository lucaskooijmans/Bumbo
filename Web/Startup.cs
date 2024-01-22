using System.Globalization;
using BusinessLogic.Services.CaoService;
using BusinessLogic.Services.CaoService.Factories;
using BusinessLogic.Services.CaoService.Interfaces;
using BusinessLogic.Services.CaoService.Rules;
using BusinessLogic.Services.HoursCalculationService;
using BusinessLogic.Services.HoursCalculationService.Factories;
using BusinessLogic.Services.HoursCalculationService.Interfaces;
using BusinessLogic.Services.HoursCalculationService.Policies;
using Data;
using Data.Interfaces;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.ViewComponents;


namespace Web;

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
        services.AddDbContext<BumboContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("BumboContext")));

        services.AddSingleton<CaoServiceFactory>();
        services.AddSingleton<HoursPolicyFactory>();
        services.AddScoped<INormRepository, NormRepository>();
        services.AddScoped<IPrognosisRepository, PrognosisRepository>();
        services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IShiftRepository, ShiftRepository>();
        services.AddScoped<IAbsenceRepository, AbsenceRepository>();
        services.AddScoped<ISchoolHoursRepository, SchoolHourRepository>();
        services.AddScoped<IRegisteredHourRepository, RegisteredHourRepository>();
        services.AddTransient<ICaoService, GeneralCaoService>();
        services.AddTransient<IHourPolicy, SaturdayHoursPolicy>();
        services.AddTransient<HoursCalculationManager>();
        services.AddScoped<IShiftManager, ShiftManager>();
        services.AddScoped<OpenAbsenceRequests>();
        services.AddScoped<DataSeeder>();




        services.AddControllersWithViews();

        services.AddIdentity<AppUser, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = false; })
            .AddEntityFrameworkStores<BumboContext>();


        var cultureInfo = new CultureInfo("en-US");
        cultureInfo.NumberFormat.CurrencySymbol = "€";

        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,BumboContext context, DataSeeder dataSeeder)
    {
        context.Database.Migrate();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        
        dataSeeder.SeedData();
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "account",
                pattern: "{controller=Account}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "account",
                pattern: "api/{Roster=RosterController}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=ManagerDashboard}/{action=Index}/{id?}").RequireAuthorization();
        });
    }
}