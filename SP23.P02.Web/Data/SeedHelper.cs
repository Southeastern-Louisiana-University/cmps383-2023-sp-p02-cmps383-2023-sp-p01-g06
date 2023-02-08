using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.TrainStations;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Data;

public static class SeedHelper
{
    public static async Task MigrateAndSeed(DataContext dataContext)
    {
        await dataContext.Database.MigrateAsync();

        var trainStations = dataContext.Set<TrainStation>();

        if (!await trainStations.AnyAsync())
        {
            for (int i = 0; i < 3; i++)
            {
                dataContext.Set<TrainStation>()
                    .Add(new TrainStation
                    {
                        Name = "Hammond",
                        Address = "1234 Place st"
                    });
            }

            await dataContext.SaveChangesAsync();
        }

        var users = dataContext.Set<User>();

        if (!await users.AnyAsync())
        {
            dataContext.Set<User>()
                .Add(new User
                {
                    Username = "bob",
                    Password = "Password123!"
                });

            dataContext.Set<User>()
                .Add(new User
                {
                    Username = "sue",
                    Password = "Password123!"
                });

            dataContext.Set<User>()
                .Add(new User
                {
                    Username = "galkadi",
                    Password = "Password123!"
                });

        }

        var roles = dataContext.Set<Role>();

        if (!await roles.AnyAsync())
        {
            dataContext.Set<Role>()
                .Add(new Role
                {
                    Name = "Admin"
                });

            dataContext.Set<Role>()
                .Add(new Role
                {
                    Name = "User"
                });

            await dataContext.SaveChangesAsync();
        }


    }
}