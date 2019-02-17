using Microsoft.EntityFrameworkCore;
using WeddingsPlanner.Data.EntityFramework;

namespace WeddingsPlanner.Business.Tests
{
    public static class DbContextProvider
    {
        public static ApplicationDbContext GetInMemoryDbContext() =>
            new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Business.Tests").Options);
    }
}
