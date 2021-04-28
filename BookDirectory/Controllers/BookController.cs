using BookDirectory.Dtos;
using BookDirectory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDirectory.Controllers
{
    public class Database
    {
        public Book[] Books { get; set; } = new Book[0];
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private ViewBookDto[] _books;

        public BookController()
        {
            var database = JsonConvert.DeserializeObject<Database>(
                System.IO.File.ReadAllText("database.json"));

            _books = database.Books;
        }
    }
}
