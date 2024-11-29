using LibraryAppMVC.Data;
using LibraryAppMVC.IRepositories;
using LibraryAppMVC.IServices;
using LibraryAppMVC.Repositories;
using LibraryAppMVC.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using LibraryAppMVC.Utility;
using LibraryAppMVC.Models;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using LibraryAppMVC.Migrations;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

//commit test
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//registrazione dbcontext da inserire sempre prima del middleware
builder.Services.AddDbContext<LibraryDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<LibraryDbContext>().AddDefaultTokenProviders();

//sempre dopo addIdentity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddRazorPages();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();


builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); //Set your timeout here
});





var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    // Ensure the database is created and apply only the AddAuthorTable migration
//    using (var scope = app.Services.CreateScope())
//    {
//        var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

//        // Ensure the database is created if it doesn't exist
//        dbContext.Database.EnsureCreated();

//        // Apply the specific migration
//        var pendingMigrations = dbContext.Database.GetPendingMigrations();

//        if (pendingMigrations.Contains("AddAuthorTable"))
//        {
//            // Apply the specific migration manually
//            dbContext.Database.Migrate();  // This will apply the pending migrations including AddAuthorTable
//        }
//    }
//}

//punto 2 - creare il db con una migration di default
//if (app.Environment.IsDevelopment())
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

//        // check se db non esiste
//        if(!dbContext.Database.GetService<IRelationalDatabaseCreator>().Exists())
//        {
//            dbContext.Database.GetInfrastructure().GetService<IMigrator>().Migrate("20241108145433_AddAuthorTable");
//            //dbContext.Database.GetInfrastructure().GetService<IMigrator>().Migrate("20241108150908_AddBookTable");
//        }
//    }
//}

//punto 1 - auto-migration 
//if (app.Environment.IsDevelopment())
//{
//    using IServiceScope scope = app.Services.CreateScope();
     
//    AutoMigration.ApplyMigration<LibraryDbContext>(scope);
//}

//punto 3 - check migration da script sql in una cartella
#region checkmigrations
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

        var migrationChecker = new CheckMigrations(dbContext);

        var abs_path = app.Environment.ContentRootPath;

        string migrationsFolderPath = Path.Combine(abs_path, "scripts"); ;
        string outputFilePath = Path.Combine(abs_path, "scripts", "migration_results.txt");

        // Check migrations and write results to a text file
        migrationChecker.CheckAndWriteToFile(migrationsFolderPath, outputFilePath);
    }

}
#endregion 


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var context = services.GetRequiredService<LibraryDbContext>();
//    //context.Database.EnsureCreated();
//    DbInitializer.Initialize(context);
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //sempre prima di authorization
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
