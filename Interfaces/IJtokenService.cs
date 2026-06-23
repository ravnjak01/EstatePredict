using EstatePredict.Entities;
namespace EstatePredict.Services.Interfaces;
public interface IJtokenService
{
    string CreateToken(ApplicationUser user);
}