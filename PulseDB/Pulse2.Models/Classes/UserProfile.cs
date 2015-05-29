using System;
using NHibernate.Validator.Constraints;
using Pulse2DataLayer.Models.Interfaces;

namespace Pulse2DataLayer.Models.Classes
{
    public class UserProfile : IModel
    {
        [NotNullNotEmpty]
        public virtual Guid UserId { get; set; }
        public virtual int UserProfileId { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
    }
}