using Microsoft.EntityFrameworkCore;

namespace LibraryAppMVC.Utility
{
    public static class AutoMigration
    {
        public static void ApplyMigration<TDbContext>(IServiceScope scope)
            where TDbContext : DbContext
        {
            using TDbContext context = scope.ServiceProvider
                .GetRequiredService<TDbContext>();

            if(context.Database.GetPendingMigrations().Any()) context.Database.Migrate();

        }
    }
}
