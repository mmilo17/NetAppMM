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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.OrderByDescending(u => u.LastActive).AsQueryable();

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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

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

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages.AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.MessageRecipientId == messageParams.UserId && u.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.MessageSenderId == messageParams.UserId && u.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(u => u.MessageRecipientId == messageParams.UserId && u.RecipientDeleted == false && u.IsRead == false);
                    break;    
            }    

            messages = messages.OrderByDescending(d => d.MessageSent);
            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);  
        }
        

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
                .Where(m => m.MessageRecipientId == userId && m.RecipientDeleted == false && m.MessageSenderId == recipientId 
                    || m.MessageRecipientId == recipientId && m.MessageSenderId == userId && m.SenderDeleted == false)
                .OrderByDescending(m => m.MessageSent)
                .ToListAsync();

            return messages;
        }
    }
}