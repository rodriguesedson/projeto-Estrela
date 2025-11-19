using api.Enums;
using api.Interfaces;
using api.Mappers;
using api.Models.Dtos;
using api.Models.Requests;
using api.Models.Responses;

namespace api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ISendEmailService _emailService;
    
    public UserService(IUserRepository userRepository, ISendEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        var list = await _userRepository.GetAllUsers();
        return UserMapper.ToUserResponseList(list.ToList());
    }

    public async Task<UserResponse> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user is null) throw new Exception("Usuário não encontrado");
        return UserMapper.ToResponse(user);
    }
    
    public async Task<string> RegisterAsync(UserRequest request)
    {
        var list = await _userRepository.GetAllUsers();
        var id = list.Count();
        var newUser = UserMapper.ToEntity(request);
        newUser.Id = id;
        newUser.Role = Role.Student;
        
        try
        {
            var search = await _userRepository.GetUserByEmailAsync(newUser.Email);
            if (search is not null) throw new Exception("Email já registrado");
        
            var response = await _userRepository.RegisterUser(newUser);
            const string SUBJECT = "Nova conta";
            const string BODY = "Nova conta criada com sucesso! Use o email cadastrado para acessá-la";
            var newEmail = new EmailDto(newUser.Email, SUBJECT, BODY);
            _emailService.SendTestMessage(newEmail);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception("Não foi possível registrar o usuário: " + ex.Message);
        }
    }
}