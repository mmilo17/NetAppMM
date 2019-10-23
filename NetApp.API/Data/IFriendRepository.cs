using System.Collections.Generic;
using System.Threading.Tasks;
using NetApp.API.Models;

namespace NetApp.API.Data
{
    public interface IFriendRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
    }
}