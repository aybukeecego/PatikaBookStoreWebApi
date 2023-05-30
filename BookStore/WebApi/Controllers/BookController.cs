using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.GetBookDetail;
using static WebApi.BookOperations.GetBookDetail.GetBookDetailQuery;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation;
using static WebApi.UpdateBook.UpdateBookCommand;
using WebApi.UpdateBook;

namespace WebApi.Controllers;

[ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]

        public IActionResult GetBook()
        {
            GetBooksQuery query=new GetBooksQuery(_context,_mapper);
            var result =query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
          GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
          GetBookDetailViewModel result;
          try
          {
            query.bookId=id;
            GetBookDetailQueryValidation validator= new GetBookDetailQueryValidation();
            validator.ValidateAndThrow(query);
             result= query.Handle();
          }
          catch (Exception ex)
          {

            return BadRequest(ex.Message);

          }


            return Ok(result);

        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command= new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model=newBook;
                CreateBookCommandValidation validator=new CreateBookCommandValidation();
                validator.ValidateAndThrow(command);
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
                UpdateBookCommandValidation validator= new UpdateBookCommandValidation();
                validator.ValidateAndThrow(command);
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
            DeleteBookCommand command= new DeleteBookCommand(_context);

            try
            {
                command.bookId=id;
                DeleteBookCommandValidation validator= new DeleteBookCommandValidation();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
                       
            }

            return Ok();
        }



    }
    
