using System.ComponentModel.DataAnnotations;

namespace DomainAPI.DTO
{
    public class ListingDTO
    {
        public long Id { get; set; }

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Suburb { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;

        [Required]
        public int Postcode { get; set; }

    }
}
