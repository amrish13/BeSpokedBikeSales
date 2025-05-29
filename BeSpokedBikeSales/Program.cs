using BeSpokedBikeSales.Data;
using BeSpokedBikeSales.Interface;
using BeSpokedBikeSales.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BeSpokedBikeSalesContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("BeSpokedBikeSalesContext") ?? throw new InvalidOperationException("Connection string 'BeSpokedBikeSalesContext' not found."), 
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("BeSpokedBikeSalesContext"))));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ISalesPersonService, SalesPersonService>();
builder.Services.AddTransient<IProductsService, ProductsService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ISalesService, SalesService>();
builder.Services.AddTransient<IDiscountService, DiscountService>();
builder.Services.AddTransient<IReportService, ReportService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
