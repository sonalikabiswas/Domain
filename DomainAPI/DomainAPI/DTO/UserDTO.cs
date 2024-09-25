using System.ComponentModel.DataAnnotations;

namespace DomainAPI.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        public string? Gender { get; set; }

        public string? Occupation { get; set; }
    }
}
