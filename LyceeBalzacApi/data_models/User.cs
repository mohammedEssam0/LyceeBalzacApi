using System.ComponentModel.DataAnnotations;

namespace LyceeBalzacApi.data_models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public byte[] Salt { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public UserStatus UserStatus { get; set; }
    public Role Role { get; set; }

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