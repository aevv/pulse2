using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using Pulse2DataLayer.Models.Interfaces;

namespace Pulse2DataLayer.Models.Classes
{
    public class Role : IModel
    {
        public virtual Guid RoleId { get; set; }

        [NotNullNotEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<UsersInRole>  UserInRoles { get; set; }

        public Role()
        {
            UserInRoles = new List<UsersInRole>();
        }
    }
}
