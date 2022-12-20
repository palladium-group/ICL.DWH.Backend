using ICL.DWH.Backend.Core;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ICL.DWH.Backend
{
    public static class EnsureMigration
    {
        public static void EnsureMigrationOfContext<T>(IApplicationBuilder app) where T : DataContext
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var contextName = typeof(T).Name;
                Log.Debug($"initializing Database context: {contextName}");
                var context = serviceScope.ServiceProvider.GetService<T>();
                try
                {
                    if (context != null)
                    {
                        context.Database.Migrate();
                        context.EnsureSeeded();
                    }
                    Log.Debug($"initializing Database context: {contextName} [OK]");
                }
                catch (Exception e)
                {
                    var msg = $"initializing Database context: {contextName} Error";
                    Log.Error(msg, e);
                }
            }
        }
    }
}
