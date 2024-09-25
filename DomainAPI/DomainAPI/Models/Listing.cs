using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainAPI.Models
{
    public partial class Listing
    {
        public Listing()
        {
            SavedListings = new HashSet<SavedListing>();
            UserListings = new HashSet<UserListing>();
        }

        public long Id { get; set; }

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Suburb { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;

        [Required]
        public int Postcode { get; set; }

        public virtual ICollection<SavedListing> SavedListings { get; set; }
        public virtual ICollection<UserListing> UserListings { get; set; }
    }
}
