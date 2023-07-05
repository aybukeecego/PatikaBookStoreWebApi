
using WebApi.DbOperations;
using Microsoft.AspNetCore.Mvc;
using System;
namespace WebApi.Application.BookOperations.DeleteBook;

public class DeleteBookCommand
{
    private readonly BookStoreDbContext _dbcontext;

    public int bookId {get; set;}

    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
        _dbcontext=dbContext;
    }

    public void Handle()
    {
            var book=_dbcontext.Books.SingleOrDefault(x=> x.Id==bookId);

            if(book==null)
            throw new InvalidOperationException("silmek istediğiniz ürüne ait id bulunamadı");

            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
    }



}