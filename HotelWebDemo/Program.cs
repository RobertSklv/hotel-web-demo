using System.Globalization;
using HotelWebDemo.Configuration;
using HotelWebDemo.Data;
using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Display;
using StarExplorerMainServer.Areas.Admin.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, p => p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

//Cookie Authentication
builder.Services.AddAuthentication()
    .AddCookie("AdminCookie", options =>
    {
        options.Cookie.Name = "AdminAuth";
        options.LoginPath = "/Admin/Login";
    })
    .AddCookie("CustomerCookie", options =>
    {
        options.Cookie.Name = "CustomerAuth";
        options.LoginPath = "/Login";
    });

CultureInfo culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminAuthService, AdminAuthService>();
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IAdminUserCreateService, AdminUserCreateService>();
builder.Services.AddScoped<IAdminUserService, AdminUserService>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IRoomFeatureService, RoomFeatureService>();
builder.Services.AddScoped<IRoomFeatureRepository, RoomFeatureRepository>();
builder.Services.AddScoped<IRoomCategoryService, RoomCategoryService>();
builder.Services.AddScoped<IRoomCategoryRepository, RoomCategoryRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingTotalsService, BookingTotalsService>();
builder.Services.AddScoped<IBookingLogService, BookingLogService>();
builder.Services.AddScoped<IBookingLogRepository, BookingLogRepository>();
builder.Services.AddScoped<IRoomReservationService, RoomReservationService>();
builder.Services.AddScoped<IRoomReservationRepository, RoomReservationRepository>();

builder.Services.AddScoped<IViewRenderService, ViewRenderService>();
builder.Services.AddTransient<IMailingService, MailingService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddScoped<ILinkGeneratorSerivce, LinkGeneratorService>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IEntityFilterService, EntityFilterService>();
builder.Services.AddScoped<IEntitySortService, EntitySortService>();
builder.Services.AddScoped<IEntitySearchService, EntitySearchService>();
builder.Services.AddScoped<IAdminPageService, AdminPageService>();
builder.Services.AddScoped<IEntityHelperService, EntityHelperService>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

MessageTemplateTextFormatter logFormatter = new("[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] {Message:lj}{NewLine}{Exception}");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
        .WriteTo.RollingFile(@$"Logs\Info.log", retainedFileCountLimit: 7))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug || e.Level == LogEventLevel.Error || e.Level == LogEventLevel.Fatal)
        .WriteTo.RollingFile(@$"Logs\Debug.log", retainedFileCountLimit: 7))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error || e.Level == LogEventLevel.Fatal)
        .WriteTo.RollingFile(logFormatter, @"Logs\Error.log", retainedFileCountLimit: 7))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
        .WriteTo.RollingFile(@$"Logs\Fatal.log", retainedFileCountLimit: 7))
    .CreateLogger();

builder.Services.AddSingleton(Log.Logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
