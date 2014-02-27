//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Renterator.DataAccess.Model
{
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
            this.Accounts = new HashSet<Account>();
        }
    
        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public System.DateTime LastLoginDate { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
