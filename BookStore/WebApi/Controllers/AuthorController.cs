using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using AutoMapper;
using static WebApi.Application.AuthorOperations.Queries.GetAuthors.GetAuthorQuery;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;

namespace WebApi.Controllers;

[ApiController]

[Route("[controller]s")]

public class AuthorController:ControllerBase
{
    private readonly IMapper _mapper;

    private readonly BookStoreDbContext _context;

    public AuthorController(IMapper mapper, BookStoreDbContext context)
    {
        _mapper=mapper;
        _context=context;

    }

    [HttpGet]

        public IActionResult GetAuthor()
        {
            GetAuthorQuery query=new GetAuthorQuery(_context,_mapper);
            var result =query.Handle();
            return Ok(result);
        }
}
