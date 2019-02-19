using Microsoft.EntityFrameworkCore;
using WeddingsPlanner.Data.EntityFramework;

namespace WeddingsPlanner.Business.Tests
{
    public static class DbContextProvider
    {
        private const string TestDbConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=TestWeddingsPlannerDb;" +
            "Integrated Security=True;" +
            "Trusted_Connection=True;" +
            "MultipleActiveResultSets=true;";

        public static ApplicationDbContext GetSqlServerDbContext() =>
            new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString:TestDbConnectionString)
                .Options);
    }
}
