using WebApi.Common;
using WebApi.DbOperations;
using Microsoft.AspNetCore.Mvc;
using System;
namespace WebApi.BookOperations.CreateBook;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }

    private readonly BookStoreDbContext _dbcontext;
    public CreateBookCommand(BookStoreDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public void Handle()
    {
            var book=_dbcontext.Books.SingleOrDefault(x=> x.Title==Model.Title);
            if(book is not null)
            throw new InvalidOperationException("eklemek istediğiniz kitap zaten mevcut");

            book=new Book();
            book.Title=Model.Title;
            book.GenreId=Model.GenreId;
            book.PageCount=Model.PageCount;
            book.PublishDate=Model.PublishDate;

            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();

    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public int GenreId { get; set; }

        public DateTime PublishDate { get; set; }

    }

}