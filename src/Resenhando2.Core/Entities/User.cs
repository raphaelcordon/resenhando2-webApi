using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Resenhando2.Core.Entities;

public class User : IdentityUser<Guid>
{
    [Required(ErrorMessage = "Mandatory Field")]
    [MinLength(2, ErrorMessage = "Field must have at least 2 characters")]
    [MaxLength(50, ErrorMessage = "Field must have at least 2 characters")]
    public required string  FirstName { get; set; }
    
    [Required(ErrorMessage = "Mandatory Field")]
    [MinLength(2, ErrorMessage = "Field must have at least 2 characters")]
    [MaxLength(50, ErrorMessage = "Field must have at least 2 characters")]
    public required string LastName { get; set; }
}