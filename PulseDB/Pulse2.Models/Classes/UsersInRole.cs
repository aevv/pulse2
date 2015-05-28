using System;
using Pulse2DataLayer.Models.Interfaces;

namespace Pulse2DataLayer.Models.Classes
{
    public class UsersInRole : IModel
    {
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Guid RoleId { get; set; }
        public virtual Role Role { get; set; }


        public override bool Equals(object obj)
        {
            var target = obj as UsersInRole;
            if (target == null)
                return false;
            if (UserId == target.UserId && RoleId == target.RoleId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = GetType().GetHashCode();
            hashCode = (hashCode*397) ^ UserId.GetHashCode();
            hashCode = (hashCode*397) ^ RoleId.GetHashCode();
            return hashCode;
        }
    }
}