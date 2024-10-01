using System.Text;

namespace DTO;

public class UserDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool IsActived { get; set; } = true;
    public string EncryptPassword(string password)
    {
        string salt = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        return salt;
    }
}