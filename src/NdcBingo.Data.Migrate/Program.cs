using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using RendleLabs.EntityFrameworkCore.MigrateHelper;

namespace NdcBingo.Data.Migrate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var loggerFactory = new LoggerFactory().AddConsole((_, level) => true);
            await new MigrationHelper(loggerFactory).TryMigrate(args);
            Console.WriteLine("Migration complete.");
        }
    }
    
    public class DesignTimeNoteContextFactory : IDesignTimeDbContextFactory<BingoContext>
    {
        public const string LocalPostgres = "Host=localhost;Database=bingo;Username=bingo;Password=SecretSquirrel";

        public static readonly string MigrationAssemblyName =
            typeof(DesignTimeNoteContextFactory).Assembly.GetName().Name;

        public BingoContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder()
                .UseNpgsql(args.FirstOrDefault() ?? LocalPostgres, b => b.MigrationsAssembly(MigrationAssemblyName));
            return new BingoContext(builder.Options);
        }
    }
}
