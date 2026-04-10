using Microsoft.EntityFrameworkCore;
using VitakorProb.Context;
using VitakorProb.Interfaces;
using VitakorProb.Model;

namespace VitakorProb.Service;

public class UserService : IUserService
{
    AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User> Login(User user)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (user.Password == existingUser.Password)
        {
            return existingUser;
        }
        
        throw new Exception("Invalid credentials");
    }

    public async Task<User> Signup(User user)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        
        if (existingUser != null)
        {
            throw  new Exception("User already exists");
        }
        
        var createdUser = await _dbContext.Users.AddAsync(user);
        
        await _dbContext.SaveChangesAsync();
        
        return createdUser.Entity;
    }
}