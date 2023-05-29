using WebApi.Common;
using WebApi.DbOperations;
using Microsoft.AspNetCore.Mvc;
using System;
namespace WebApi.BookOperations.GetBookDetail;

public class GetBookDetailQuery
{
    public int bookId {get;set;}
    private readonly BookStoreDbContext _dbcontext;

    public GetBookDetailQuery(BookStoreDbContext dbcontext)
    {
       _dbcontext=dbcontext;
    }
    public GetBookDetailViewModel Handle()
    {
        var book = _dbcontext.Books.Where(book=> book.Id==bookId).SingleOrDefault();
        if(book==null)
        throw new InvalidOperationException("Bu id'ye ait ürün bulunamadı");
        
        GetBookDetailViewModel vm=new GetBookDetailViewModel();

        vm.Title=book.Title;
        vm.Genre=((GenreEnum)book.GenreId).ToString();
        vm.PageCount=book.PageCount;

        return vm;
    }

    public class GetBookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }

        public string Genre { get; set; }

    }

}