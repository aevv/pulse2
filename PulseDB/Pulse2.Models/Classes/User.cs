using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using Pulse2DataLayer.Models.Interfaces;

namespace Pulse2DataLayer.Models.Classes
{
    public class User : IModel
    {
        public virtual Guid UserId { get; set; }
        [NotNullNotEmpty]
        public virtual string Username { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime LastActivityDate { get; set; }
        public virtual IList<Membership> Memberships { get; set; }
        public virtual IList<UserProfile> Profiles { get; set; } 
        public virtual IList<UsersInRole> UserInRoles { get; set; } 

        public User()
        {
            Memberships = new List<Membership>();
            Profiles = new List<UserProfile>();
            UserInRoles = new List<UsersInRole>();
        }
    }
}
