using UniSync.Domain.Entities;
using UniSync.Infrastructure;

namespace GlobalBuy.Ticket.API.IntegrationTests.Base
{
    public class Seed
    {
        public static void InitializeDbForTests(UniSyncContext context)
        {
            var categories = new List<Category>
            {
                Category.Create("Concerts").Value,
                Category.Create("Sports").Value,
                Category.Create("Theater").Value,
                Category.Create("Comedy").Value
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}
