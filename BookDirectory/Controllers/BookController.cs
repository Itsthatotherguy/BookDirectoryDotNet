using AutoMapper;
using BookDirectory.Data;
using BookDirectory.Dtos;
using BookDirectory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppDbContext _dbContext;

        public BookController(IMapper mapper, IAppDbContext dbContext)
        {            
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all Books
        /// </summary>
        /// <returns>All Books</returns>
        /// <response code="200">Returns all Books</response>
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _dbContext.Books.ToArrayAsync();

            return Ok(_mapper.Map<ViewBookDto[]>(books));
        }

        /// <summary>
        /// Gets one Book by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Book</returns>
        /// <response code="200">If the Book is found</response>
        /// <response code="400">If the Book is not found</response>
        [HttpGet("{id}", Name = "GetOne")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (book == null)
            {
                return BadRequest($"Book with ID {id} doesn't exist in the database");
            }

            //return CreatedAtRoute("GetOne", new { Id = id }, book);
            return Ok(_mapper.Map<ViewBookDto>(book));
        }

        /// <summary>
        /// Creates a new book
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A path to the newly created book</returns>
        /// <response code="201"></response>
        /// <response code="400">If invalid input is provided</response>
        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);

            try
            {
                _dbContext.Books.Add(book);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return CreatedAtRoute("GetOne", new { Id = book.Id }, book);
        }

        /// <summary>
        /// Updates a book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <response code="204">If successful</response>
        /// <response code="400">If invalid input is provided</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, UpdateBookDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("The ID in the url doesn't match the ID in the request");
            }

            var book = _dbContext.Books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            {
                return BadRequest($"Book with ID {id} doesn't exist in the database");
            }

            _mapper.Map(dto, book);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a book
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">If successful</response>
        /// <response code="400">If invalid input is provided</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = _dbContext.Books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            {
                return BadRequest($"Book with ID {id} doesn't exist in the database");
            }

            try
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return NoContent();
        }
    }
}
