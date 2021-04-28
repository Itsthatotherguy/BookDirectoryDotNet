using BookDirectory.Dtos;
using BookDirectoryDotNet.Tests.Fixtures;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookDirectoryDotNet.Tests
{
    public class BookControllerTests : BaseIntegrationTest
    {
        public BookControllerTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async void GetBooks_ShouldReturnBooks()
        {
            var response = await _httpClient.GetAsync("api/Book");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var books = await DeserializeJson<ViewBookDto[]>(response);
            books.Should().HaveCount(3);
        }

        [Fact]
        public async void GetBook_ShouldReturnBook()
        {
            var getAllBooksResponse = await _httpClient.GetAsync("api/Book");

            var books = await DeserializeJson<ViewBookDto[]>(getAllBooksResponse);

            var id = books[0].Id;

            var response = await _httpClient.GetAsync($"api/Book/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var book = await DeserializeJson<ViewBookDto>(response);
            book.Id.Should().Be(id);
        }

        [Fact]
        public async void CreateBook_ShouldReturnCreatedRoute()
        {
            var dto = new CreateBookDto
            {
                Title = "The Chronicles of Narnia",
                Author = "C.S. Lewis",
                Edition = "Geoffrey Bles",
                Price = 100,
                YearWritten = 1950
            };

            var content = SerializeJson(dto);

            var response = await _httpClient.PostAsync("api/Book", content);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var getAllResponse = await _httpClient.GetAsync("api/Book");

            var books = await DeserializeJson<ViewBookDto[]>(getAllResponse);
            books.Should().HaveCount(4);
        }

        [Fact]
        public async void UpdateBook_ShouldReturnNoContent()
        {
            var getAllBooksResponse = await _httpClient.GetAsync("api/Book");

            var books = await DeserializeJson<ViewBookDto[]>(getAllBooksResponse);

            var oldBook = books[2];

            var dto = new UpdateBookDto
            {
                Id = oldBook.Id,
                //Title = oldBook.Title,
                Author = "J.R. Tolkien",
                //Edition = oldBook.Edition,
                //Price = oldBook.Price,
                //YearWritten = oldBook.YearWritten
            };

            var content = SerializeJson(dto);

            var response = await _httpClient.PutAsync($"api/Book/{oldBook.Id}", content);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var getBookResponse = await _httpClient.GetAsync($"api/Book/{oldBook.Id}");
            var book = await DeserializeJson<ViewBookDto>(getBookResponse);

            book.Author.Should().Be("J.R. Tolkien");
        }

        private async Task<T> DeserializeJson<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(
                await response.Content.ReadAsStringAsync());
        }

        private StringContent SerializeJson<T>(T content)
        {
            return new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                "application/json");
        }
    }
}
