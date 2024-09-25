using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DomainAPI.Models
{
    public partial class User
    {
        public User()
        {
            SavedListings = new HashSet<SavedListing>();
            UserListings = new HashSet<UserListing>();
        }

        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        public string? Gender { get; set; }

        public string? Occupation { get; set; }
        public virtual ICollection<SavedListing> SavedListings { get; set; }
        public virtual ICollection<UserListing> UserListings { get; set; }
    }
}
