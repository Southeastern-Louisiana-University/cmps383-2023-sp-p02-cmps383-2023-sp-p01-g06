using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// sets up our database connection
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

//Look at these
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<DataContext>();
//

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    await SeedHelper.MigrateAndSeed(db);

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await roleManager.CreateAsync(new Role
    { Name = "Admin" });

    await roleManager.CreateAsync(new Role
    {
        Name = "User"
    });

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await userManager.CreateAsync(new User
    {
        Username = "bob"
    }, "Password123!");

    await userManager.CreateAsync(new User
    {
        Username = "sue"
    }, "Password123!");

    await userManager.CreateAsync(new User
    {
        Username = "galkadi"
    }, "Password123!");

    var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<User>>();


    //await signInManager.SignInAsync(new User
    //{
    //    Username = "bob",
    //    Password = "Password123!"
    //}, true);


}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//see: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0
// Hi 383 - this is added so we can test our web project automatically. More on that later
public partial class Program { }