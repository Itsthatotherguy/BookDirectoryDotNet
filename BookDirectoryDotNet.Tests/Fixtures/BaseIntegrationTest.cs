using BookDirectory.Data;
using BookDirectory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookDirectoryDotNet.Tests.Fixtures
{
    public abstract class BaseIntegrationTest : IClassFixture<ApiWebApplicationFactory>, IDisposable
    {
        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _httpClient;
        protected AppDbContext _dbContext;

        private readonly IConfiguration _configuration;
        
        protected BaseIntegrationTest(ApiWebApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("integrationtestsettings.json")
                .Build();

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("Application"))
                .Options;

            _dbContext = new AppDbContext(dbContextOptions);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.Migrate();

            SeedBooks();
        }

        private void SeedBooks()
        {
            _dbContext.Books.AddRange(new Book[]
            {
                new Book { Title = "One Hundred Years of Solitude", Author = "Marquez", YearWritten = 1967, Edition = "Harper Perennial", Price = 14.0 },
                new Book { Title = "Hamlet, Prince of Denmark", Author = "William Shakespear", YearWritten = 1603, Edition = "Signet Classics", Price = 7.95 },
                new Book { Title = "Lord of the Rings", Author = "J.R.R. Tolkien", YearWritten = 1937, Edition = "Penguin", Price = 27.45 }
            });

            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}
