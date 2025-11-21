using System.Net;
using api.Entities;

namespace api.Utils;

public static class UserValidator
{
    public static void UserNotFound(User user)
    {
        if (user is null) throw new CustomException(HttpStatusCode.NotFound, "Usuário não encontrado");
    }

    public static void EmailAlreadyInUse(User user)
    {
        if (user is not null) throw new CustomException(HttpStatusCode.UnprocessableEntity, "Email já cadastrado");
    }

    public static void UserAlreadyActive(User user)
    {
        if (user.IsActive)
            throw new CustomException(HttpStatusCode.UnprocessableEntity, "Usuário já se encontra ativo");
    }

    public static void UserAlreadyInactive(User user)
    {
        if (!user.IsActive)
            throw new CustomException(HttpStatusCode.UnprocessableEntity, "Usuário já se encontra inativo");
    }
}