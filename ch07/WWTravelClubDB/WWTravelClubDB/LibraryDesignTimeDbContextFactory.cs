using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WWTravelClubDB
{
    public class LibraryDesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<MainDBContext>
    {
        private const string endpoint = "https://533a17b5-0ee0-4-231-b9ee.documents.azure.com:443/";
        private const string key = "Uvt6140ss4iYd4yWxJpbpYRmD3BKPBXOotC1TvqM6OsnPuWgRxSD1ESXemkDn9M7LLxWh5bcZLAtBvOl5gvOYw==";
        private const string datbaseName = "packagesdb";
        public MainDBContext CreateDbContext(params string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainDBContext>();

            builder.UseCosmos(endpoint, key, datbaseName);
            return new MainDBContext(builder.Options);
        }
    }
}
