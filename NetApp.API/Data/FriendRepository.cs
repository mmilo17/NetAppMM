using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetApp.API.Helpers;
using NetApp.API.Models;

namespace NetApp.API.Data
{
    public class FriendRepository : IFriendRepository
    {
        private readonly DataContext _context;
        public FriendRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id ==id);

            return photo;
        }

        public async Task<Request> GetRequest(int userId, int recipientId)
        {
            return await _context.Requests.FirstOrDefaultAsync(u => u.SenderId == userId && u.ReceiverId == recipientId);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            users = users.Where(u => u.Gender != userParams.Gender);
            
            if (userParams.Senders)
            {
                var userSenders = await GetUserRequests(userParams.UserId, userParams.Senders);
                users = users.Where(u => userSenders.Contains(u.Id));
            }

            if (userParams.Recivers)
            {
                var userRecivers = await GetUserRequests(userParams.UserId, userParams.Senders);
                users = users.Where(u => userRecivers.Contains(u.Id));
            }
            
            if (userParams.MinAge != 1 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users =users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserRequests(int id, bool senders)
        {
            var user = await _context.Users
                .Include(x => x.Senders)
                .Include(x => x.Recivers)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (senders) 
            {
                return user.Senders.Where(u => u.ReceiverId == id).Select(i => i.SenderId);
            }
            else
            {
                return user.Recivers.Where(u => u.SenderId == id).Select(i => i.ReceiverId);
            }   
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}