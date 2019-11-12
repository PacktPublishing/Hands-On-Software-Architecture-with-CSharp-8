using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WWTravelClubDB
{
    public class LibraryDesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<MainDBContext>
    {
        private const string endpoint = "insert here your account UTL";
        private const string key = "insert here your key";
        private const string datbaseName = "packagesdb";
        public MainDBContext CreateDbContext(params string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainDBContext>();

            builder.UseCosmos(endpoint, key, datbaseName);
            return new MainDBContext(builder.Options);
        }
    }
}
