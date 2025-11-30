using System.Net;
using api.Enums;
using api.Interfaces;
using api.Mappers;
using api.Models.Dtos;
using api.Models.Requests;
using api.Models.Responses;
using api.Utils;

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
        UserValidator.UserNotFound(user);
        return UserMapper.ToResponse(user);
    }

    public async Task<UserResponse> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        UserValidator.UserNotFound(user);
        return UserMapper.ToResponse(user);
    }
    
    public async Task<UserResponse> RegisterAsync(UserRequest request)
    {
        var newUser = UserMapper.ToEntity(request);
        newUser.Role = Role.Student;
        newUser.IsActive = true;
        
        try
        {
            var search = await _userRepository.GetUserByEmailAsync(newUser.Email);
            UserValidator.EmailAlreadyInUse(search);
        
            await _userRepository.RegisterUser(newUser);
            const string SUBJECT = "Nova conta";
            const string BODY = "Nova conta criada com sucesso! Use o email cadastrado para acessá-la";
            var newEmail = new EmailDto(newUser.Email, SUBJECT, BODY);
            _emailService.SendMessage(newEmail);
            return UserMapper.ToResponse(newUser);
        }
        catch (Exception ex)
        {
            throw new CustomException(HttpStatusCode.UnprocessableEntity, "Não foi possível registrar o usuário: " + ex.Message);
        }
    }

    public async Task<UserResponse> UpdateAsync(Guid id, UserUpdateRequest request)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (!user.Email.Equals(request.Email))
        {
            var search = await _userRepository.GetUserByEmailAsync(request.Email);
            UserValidator.EmailAlreadyInUse(search);
        }
        user.Email = request.Email;
        user.Name = request.Name;
        user.BirthDate = request.BirthDate;

        try
        {
            await _userRepository.EditUser(user);
            const string SUBJECT = "Edição de cadastro";
            const string BODY = "Conta editada com sucesso! Use o email cadastrado para acessá-la";
            var newEmail = new EmailDto(user.Email, SUBJECT, BODY);
            _emailService.SendMessage(newEmail);
            return  UserMapper.ToResponse(user);
        }
        catch (Exception ex)
        {
            throw new CustomException(HttpStatusCode.UnprocessableEntity, "Não foi possível editar o usuário: " + ex.Message);
        }
    }

    public async Task<UserResponse> DeactivateAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        UserValidator.UserNotFound(user);
        UserValidator.UserAlreadyInactive(user);
        user.IsActive = false;
        try
        {
            await _userRepository.EditUser(user);
            return UserMapper.ToResponse(user);
        }
        catch (Exception ex)
        {
            throw new CustomException(HttpStatusCode.UnprocessableEntity, "Não foi possível desativar o usuário: " + ex.Message);
        }
    }

    public async Task<UserResponse> ReactivateAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        UserValidator.UserNotFound(user);
        UserValidator.UserAlreadyActive(user);
        user.IsActive = true;
        try
        {
            await _userRepository.EditUser(user);
            return UserMapper.ToResponse(user);
        }
        catch (CustomException ex)
        {
            throw new CustomException(HttpStatusCode.UnprocessableEntity, "Não foi possível reativar o usuário: " + ex.Message);
        }
    }
}