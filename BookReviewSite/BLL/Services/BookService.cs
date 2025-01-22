using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookService
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Book, BookDTO>();
                cfg.CreateMap<BookDTO, Book>();
            });
            return new Mapper(config);
        }

        public static List<BookDTO> Get()
        {
            var repo = DataAccess.BookRepo();
            return GetMapper().Map<List<BookDTO>>(repo.Get());
        }

        public static BookDTO Get(int id)
        {
            var repo = DataAccess.BookRepo();
            var book = repo.Get(id);
            return GetMapper().Map<BookDTO>(book);
        }

        public static bool Add(BookDTO book)
        {
            var repo = DataAccess.BookRepo();
            var entity = GetMapper().Map<Book>(book);
            return repo.Create(entity);
        }

        public static bool Update(int id, BookDTO book)
        {
            var repo = DataAccess.BookRepo();
            var entity = GetMapper().Map<Book>(book);
            entity.Id = id;
            return repo.Update(entity);
        }

        public static bool Delete(int id)
        {
            var repo = DataAccess.BookRepo();
            return repo.Delete(id);
        }
    }
}
