using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LyceeBalzacApi.Response;

namespace LyceeBalzacApi.data_models;

public class User
{
    [Key] public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public byte[] Salt { get; set; } = Array.Empty<byte>();
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public UserStatus UserStatus { get; set; } = UserStatus.Pending;
    public Role Role { get; set; } = Role.Student;

    public GetUserResponse ToUserResponse() =>
        new GetUserResponse(FirstName, LastName, Email, Role, PhoneNumber, Address);
}

public enum Role
{
    Admin,
    Teacher,
    Student,
    Parent
}

public enum UserStatus
{
    Pending,
    Active,
    Blocked,
    Inactive
}