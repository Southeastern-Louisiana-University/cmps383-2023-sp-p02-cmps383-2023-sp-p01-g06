using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.UserRoles;
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






builder.Services.AddControllers();


//Literally the magic line I was looking for
//I recall he mentionewd this in class and it took somew googling to find it!
//Things now seem to work again!!!
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //DataContext part
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    await SeedHelper.MigrateAndSeed(db);

    //roleManager part
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await roleManager.CreateAsync(new Role
    { 
        Name = "Admin" 
    });

    await roleManager.CreateAsync(new Role
    {
        Name = "User"
    });


    //userManager part
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    await userManager.CreateAsync(new User
    {
        UserName = "bob"
    }, "Password123!");

    await userManager.CreateAsync(new User
    {
        UserName = "sue"
    }, "Password123!");

    await userManager.CreateAsync(new User
    {
        UserName = "galkadi"
    }, "Password123!");


    var bob = db.Users.First(x => x.UserName == "bob");
    var sue = db.Users.First(x => x.UserName == "sue");
    var galkadi = db.Users.First(x => x.UserName == "galkadi");

    //userManager.Users.

    await userManager.AddToRoleAsync(bob, "User");
    await userManager.AddToRoleAsync(sue, "User");
    await userManager.AddToRoleAsync(galkadi, "Admin");


    //signInManager part


    var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<User>>();



    //This adds a role to a user


    //await signInManager.SignInAsync(bob, true);

}
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.MapControllers();

app.Run();

//see: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0
// Hi 383 - this is added so we can test our web project automatically. More on that later
public partial class Program { }