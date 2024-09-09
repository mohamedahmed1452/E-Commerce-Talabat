
using E_Commerce.Core.Models;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Repository;
using E_Commerce.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_E_CommerceProject.Service.Errors;
using Test_E_CommerceProject.Service.Extensions;
using Test_E_CommerceProject.Service.Helpers;
using Test_E_CommerceProject.Service.MiddleWares;





namespace Test_E_CommerceProject
{
    public class Program
    {

        public static async Task Main(string[] args)
          {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddApplicationServices();


            //MiddleWare 
            var app = builder.Build();



            #region Update DataBase
           
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();

            //Logger Factory ->Log Error In Kestrel 
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();//created in AddController
            try
            {
                await _dbContext.Database.MigrateAsync();//update database Done
                await StoreContextSeeding.SeedingAsync(_dbContext);//Seed Data Done
            }
            catch (Exception ex)
            {
                var Logger = loggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            }


            #endregion

            app.UseMiddleware<ExceptionMeddleWare>();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
            }

            app.UseStatusCodePagesWithRedirects("/errors/{0}");


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
