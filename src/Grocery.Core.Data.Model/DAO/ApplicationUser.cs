namespace Grocery.Core.Data.Model.DAO
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        public int BuildingNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        // Navigations Lazy Loading
        public virtual ICollection<CustomerCart> CustomerCart { get; set; }

        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
    }
}
