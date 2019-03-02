using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SpyStore.Hol.Dal.EfStructures
{
    public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var connectionString =
                @"Server=(LocalDb)\MSSQLLocalDB;Database=SpyStoreHol;MultipleActiveResultSets=true;";
            Console.WriteLine(connectionString);
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();

            optionsBuilder
                .UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            return new StoreContext(optionsBuilder.Options);
        }
    }
}
