using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperations;
public class DataGenerator {
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            // Look for any book.
            if (context.Books.Any())
            {
                return;   // Data was already seeded
            }

            context.Books.AddRange(
            new Book{
                //Id=1,
                Title="Lean Startup",
                PageCount=200,
                GenreId=1, //Personal Growth
                PublishDate=new DateTime(2001,12,03)

            },
                new Book{
                //Id=2,
                Title="Herland",
                PageCount=250,
                GenreId=2, //Science Fiction
                PublishDate=new DateTime(2001,09,06)

            },
                new Book{
                //Id=3,
                Title="Dune",
                PageCount=200,
                GenreId=2, //Science Fiction
                PublishDate=new DateTime(2001,11,21)

            } );

            context.SaveChanges();
        }
    }
}