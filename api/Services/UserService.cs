using api.Enums;
using api.Interfaces;
using api.Mappers;
using api.Models.Requests;
using api.Models.Responses;

namespace api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ISendEmailService _emailService;
    
    public UserService(IUserRepository userRepository, ISendEmailService service)
    {
        _userRepository = userRepository;
        _emailService = service;
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
    {
        var list = await _userRepository.GetAllUsers();
        return UserMapper.ToUserResponseList(list.ToList());
    }
    
    public async Task<string> RegisterUserAsync(UserRequest request)
    {
        var list = await _userRepository.GetAllUsers();
        var id = list.Count();
        var newUser = UserMapper.ToEntity(request);
        newUser.Id = id;
        newUser.Role = Role.Student;

        try
        {
            var response = await _userRepository.RegisterUser(newUser);
            return response;
        }
        catch
        {
            throw new Exception("Não foi possível registrar o usuário");
        }
    }
}