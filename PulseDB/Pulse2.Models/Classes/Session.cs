using System;
using NHibernate.Validator.Constraints;
using Pulse2DataLayer.Models.Interfaces;

namespace Pulse2DataLayer.Models.Classes
{
    public class Session : IModel
    {
        [NotNullNotEmpty]
        public virtual int SessionId { get; set; }
        [NotNullNotEmpty]
        public virtual Guid UserId { get; set; }
        [NotNullNotEmpty]
        public virtual string Token { get; set; }
        public virtual User User { get; set; }

        public override bool Equals(object obj)
        {
            var target = obj as Session;
            if (target == null)
                return false;
            if (UserId == target.UserId && SessionId == target.SessionId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = GetType().GetHashCode();
            hashCode = (hashCode*397) ^ UserId.GetHashCode();
            hashCode = (hashCode*397) ^ SessionId.GetHashCode();
            return hashCode;
        }
    }
}
