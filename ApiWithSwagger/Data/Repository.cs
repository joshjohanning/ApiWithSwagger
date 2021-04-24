using ApiWithSwagger.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithSwagger.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void UpdateRange(object[] entity);
        Task<bool> SaveAll();
        Task<User> FindById(int userId);
        Task<User> GetUser(string email);
        Task<User> GetUserWithItems(string email);
        Task<IEnumerable<User>> GetManyUsers(List<string> userEmails);
        Task<bool> CheckUser(string userEmail);
        Task<Item> GetItemById(int itemId);
        Task<IEnumerable<Item>> GetManyItemsByUser(string email);

    }
    public class Repository : IRepository
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
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

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void UpdateRange(object[] entity)
        {
            _context.UpdateRange(entity);
        }

        public async Task<User> FindById(int userId)
        {
            var user = await _context.AppUser.FindAsync(userId);
            return user;
        }

        public async Task<User> GetUser(string email)
        {
            var user = await _context.AppUser.FirstOrDefaultAsync(x => x.Email == email.ToLower());
            return user;
        }
        public async Task<User> GetUserWithItems(string email)
        {
            var userWithItems = await _context.AppUser
                .Include(i=>i.DepositItems)
                .FirstOrDefaultAsync(x => x.Email == email.ToLower());
            return userWithItems;
        }

        public async Task<IEnumerable<User>> GetManyUsers(List<string> userEmails)
        {
            var users = await _context.AppUser.Where(x => userEmails.Contains(x.Email)).ToListAsync();
            return users;
        }

        public async Task<bool> CheckUser(string userEmail)
        {
            var user = await _context.AppUser.FirstOrDefaultAsync(x => x.Email.ToLower() == userEmail.ToLower());
            return user != null;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Item> GetItemById(int itemId)
        {
            var item = await _context.Items.FindAsync(itemId);
            return item;
        }

        public async Task<IEnumerable<Item>> GetManyItemsByUser(string email)
        {
            var user = await _context.AppUser.FirstOrDefaultAsync(x => x.Email == email);

            if (user != null)
            {
                var items = user.DepositItems;
                return items;
            }
            return null;
        }


    }
}
