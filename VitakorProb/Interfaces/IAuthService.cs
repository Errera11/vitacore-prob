using System.Security.Claims;
using VitakorProb.Model;

namespace VitakorProb.Interfaces;

public interface IAuthService
{
    public ClaimsPrincipal CreateClaims(User user);
}