using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBookRepo : IRepo<Book, int, bool>
    {
        List<Book> Get();
        Book Get(int id);
        bool Create(Book book);
        bool Update(Book book);
        bool Delete(int id);

        List<Book> GetBooksByAuthorId(int authorId);
    }
}
