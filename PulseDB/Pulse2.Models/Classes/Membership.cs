using System;
using NHibernate.Validator.Constraints;
using Pulse2DataLayer.Models.Interfaces;

namespace Pulse2DataLayer.Models.Classes
{
    public class Membership : IModel
    {
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }

        [NotNullNotEmpty]
        public virtual string Password { get; set; }
        [NotNullNotEmpty]
        public virtual string PasswordSalt { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordQuestion { get; set; }
        public virtual string PasswordAnswer { get; set; }
        [NotNullNotEmpty]
        public virtual bool IsEmailVerified { get; set; }
        [NotNullNotEmpty]
        public virtual bool IsLockedOut { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime CreateDate { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime LastLoginDate { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime LastPasswordChangedDate { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime LastLockOutDate { get; set; }
        [NotNullNotEmpty]
        public virtual int FailedPasswordAttemptCount { get; set; }
        [NotNullNotEmpty]
        public virtual int FailedPasswordAnswerAttemptCount { get; set; }
    }
}