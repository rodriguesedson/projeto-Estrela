using api.Entities;
using api.Models.Requests;
using api.Models.Responses;

namespace api.Mappers;

public static class UserMapper
{
    public static User ToEntity(UserRequestDto requestDto)
    {
        return new User()
        {
            Email = requestDto.Email, 
            Name = requestDto.Name, 
            BirthDate = requestDto.BirthDate
        };
    }

    public static UserResponseDto ToResponse(User user)
    {
        return new UserResponseDto()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            BirthDate = user.BirthDate,
            Role = user.Role.ToString(),
            IsActive = user.IsActive
        };
    }

    public static List<UserResponseDto> ToUserResponseList(List<User> usersList)
    {
        var mappedList = new List<UserResponseDto>();
        usersList.ForEach(user =>
        {
            var formatedUser = new UserResponseDto()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Role = user.Role.ToString(),
                IsActive = user.IsActive
            };
            mappedList.Add(formatedUser);
        });
        return mappedList;
    }
}