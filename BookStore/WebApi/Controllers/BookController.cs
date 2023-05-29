using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.UpdateBook;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers;

[ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context=context;

        }

        [HttpGet]

        public IActionResult GetBook()
        {
            GetBooksQuery query=new GetBooksQuery(_context);
            var result =query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]

        public Book GetById(int id)
        {
          GetBookDetailQuery query = new GetBookDetailQuery(_context);

            var book = _context.Books.Where(book=> book.Id==id).SingleOrDefault();
            return book;

        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command= new CreateBookCommand(_context);
            try
            {
                command.Model=newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }
        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command= new UpdateBookCommand(_context);
            try
            {
                command.bookId=id;
                command.Model=updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            var book=_context.Books.SingleOrDefault(x=> x.Id==id);

            if(book==null)
            return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }



    }
    
