using System;
using System.Collections.Generic;

namespace DomainAPI.Models
{
    public partial class SavedListing
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public long ListingId { get; set; }

        public virtual Listing Listing { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
