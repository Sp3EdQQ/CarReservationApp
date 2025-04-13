using Microsoft.EntityFrameworkCore;
using Projekt_strona.Data;
using Projekt_strona.Repositories;
using FluentValidation.AspNetCore;
using Projekt_strona.Models.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Dodaj DbContext
builder.Services.AddDbContext<CarRentalsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodaj repozytoria
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Dodaj FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<RentalValidator>();

// Dodaj kontrolery i widoki
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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