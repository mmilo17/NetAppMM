using System.Collections.Generic;
using System.Threading.Tasks;
using NetApp.API.Models;
using NetApp.API.Helpers;

namespace NetApp.API.Data
{
    public interface IFriendRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser(int userId);
         Task<Request> GetRequest(int userId, int recipientId);
    }
}