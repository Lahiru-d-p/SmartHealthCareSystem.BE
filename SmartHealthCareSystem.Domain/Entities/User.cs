using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartHealthCareSystem.Domain.Entities;

public abstract class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string NIC { get; set; } = default!;
    public string ContactNumber { get; set; } = default!;
    public string ContactEMail { get; set; } = default!;
    public Address? Address { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
}

