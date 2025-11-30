using System.Security.Cryptography;

namespace api.Utils;

public class PasswordGenerator
{
    public static string Generate(int length = 8)
    {
        // Gera string segura com caracteres alfanum e símbolos se quiser
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var data = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(data);
        var result = new char[length];
        for (int i = 0; i < length; i++)
            result[i] = chars[data[i] % chars.Length];
        return new string(result);
    }
}