using System.Net;
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

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var list = await _userRepository.GetAllUsers();
        return UserMapper.ToUserResponseList(list.ToList());
    }

    public async Task<UserResponseDto> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        UserValidator.UserNotFound(user);
        return UserMapper.ToResponse(user);
    }

    public async Task<UserResponseDto> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        UserValidator.UserNotFound(user);
        return UserMapper.ToResponse(user);
    }
    
    public async Task<UserResponseDto> UpdateAsync(Guid id, UserUpdateRequestDto requestDto)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (!user.Email.Equals(requestDto.Email))
        {
            var search = await _userRepository.GetUserByEmailAsync(requestDto.Email);
            UserValidator.EmailAlreadyInUse(search);
        }
        user.Email = requestDto.Email;
        user.Name = requestDto.Name;
        user.BirthDate = requestDto.BirthDate;

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

    public async Task<UserResponseDto> DeactivateAsync(Guid id)
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

    public async Task<UserResponseDto> ReactivateAsync(Guid id)
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