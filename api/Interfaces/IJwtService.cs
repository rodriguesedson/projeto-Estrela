using api.Entities;

namespace api.Interfaces;

public interface IJwtService
{
    string CreateToken(User user);
}