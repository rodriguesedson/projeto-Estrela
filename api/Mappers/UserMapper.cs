using api.Entities;
using api.Models.Requests;
using api.Models.Responses;

namespace api.Mappers;

public static class UserMapper
{
    public static User ToEntity(UserRequest request)
    {
        return new User(request.Email, request.Name, request.BirthDate);
    }

    public static List<UserResponse> ToUserResponseList(List<User> usersList)
    {
        var mappedList = new List<UserResponse>();
        usersList.ForEach(user =>
        {
            var formatedUser = new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Role = user.Role.ToString()
            };
            mappedList.Add(formatedUser);
        });
        return mappedList;
    }
}