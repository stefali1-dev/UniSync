using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        return user;
    }

}