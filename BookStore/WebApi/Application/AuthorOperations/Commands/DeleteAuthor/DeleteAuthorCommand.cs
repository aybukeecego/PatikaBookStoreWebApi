using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands;


public class DeleteauthorCommand
{
    public int AuthorId { get; set; }
    private readonly BookStoreDbContext _context;

    private readonly IMapper _mapper;


    public void Handle()
    {
        var author=_context.Authors.SingleOrDefault(x=>x.Id==AuthorId);

        var authorBook=_context.Books.SingleOrDefault(x=>x.AuthorId==AuthorId);

        if(author is null)
        throw new InvalidOperationException ("Yazar mevcut değil");

        if(authorBook is not null)
        throw new InvalidOperationException("Bu yazarın yayında kitabı bulunmaktadır. Öncelikle kitabı siliniz.");

        _context.Authors.Remove(author);
        _context.SaveChanges();

    }
}