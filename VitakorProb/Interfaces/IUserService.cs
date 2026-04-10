using VitakorProb.Model;

namespace VitakorProb.Interfaces;

public interface IUserService
{
    public Task<User> Login(User user);
    public Task<User> Signup(User user);
}