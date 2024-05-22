using Biograf.Repo.Interface;
using Biograf.Repo.Models;
using Biograf.Repo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly DatabaseContext _context;

        public UserRepo(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User user)
        {
            //User newUser = new User
            //{
            //    FullName = user.FullName,
            //    Email = user.Email,
            //    Phone=user.Phone,
            //    Address = user.Address,
            //    Password = user.Password,
            //    Role = user.Role,
            //};
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> Delete(int id)
        {
            var obj = await _context.Users.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task<List<User>> Get()
        {
            return await _context.Users.Include(u=>u.movies).ToListAsync();
        }

        public async Task<User> getByemail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email );
        }
        public async Task<User> Get(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.Id==id);
        }
        public async Task<User> Update(int id, User user)
        {
            var obj = _context.Users.Find(id);
            obj.FullName = user.FullName;
            obj.Address = user.Address;
            obj.Email = user.Email;
            obj.Address=user.Address;
            obj.Phone= user.Phone;
            obj.Password=user.Password;       
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
