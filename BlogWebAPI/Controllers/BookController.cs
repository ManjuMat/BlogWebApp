using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRepoLayer;
using BookDALayer;

namespace BlogWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IRepositoryBase<Book> bookRepository = null;

        public BookController()
        {
            this.bookRepository = new RepositoryBase<Book>();
        }
        // GET: api/Books/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            return Ok(book);
        }

        [HttpGet]
        [Route("Books")]
        public IActionResult GetBooks()
        {
            try
            {
                var books = bookRepository.GetAll();
                if (books == null)
                {
                    return NotFound();
                }

                return Ok(books);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
