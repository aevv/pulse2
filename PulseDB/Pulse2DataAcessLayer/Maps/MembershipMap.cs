using System;
using FluentNHibernate.Mapping;
using Pulse2DataLayer.Models.Classes;

namespace Pulse2DataAcessLayer.Maps
{
    public class MembershipMap : ClassMap<Membership>
    {
        public MembershipMap()
        {
            Table("Membership");
            Id(membership => membership.UserId).GeneratedBy.Assigned().Column("UserId");
            References(membership => membership.UserId).Column("UserId").Not.LazyLoad().Cascade.SaveUpdate();
            Map(membership => membership.Password).Column("Password").Not.Nullable();
            Map(membership => membership.PasswordSalt).Column("PasswordSalt").Nullable();
            Map(membership => membership.Email).Column("Email").Nullable();
            Map(membership => membership.PasswordQuestion).Column("PasswordQuestion").Nullable();
            Map(membership => membership.PasswordAnswer).Column("PasswordAnswer").Nullable();
            Map(membership => membership.IsEmailVerified).Column("IsEmailVerified").Not.Nullable().Default("0");
            Map(membership => membership.IsLockedOut).Column("IsLockedOut").Not.Nullable();
            Map(membership => membership.CreateDate)
                .Column("CreateDate")
                .Not.Nullable()
                .Default(DateTime.Now.ToShortDateString());
            Map(membership => membership.LastLoginDate).Column("LastLoginDate").Not.Nullable();
            Map(membership => membership.LastPasswordChangedDate).Column("LastPasswordChangedDate").Not.Nullable();
            Map(membership => membership.LastLockOutDate).Column("LastLockOutDate").Not.Nullable();
            Map(membership => membership.FailedPasswordAttemptCount).Column("FailedPasswordAttemptCount").Not.Nullable();
            Map(membership => membership.FailedPasswordAttemptCount).Column("FailedPasswordAnswerAttemptCount").Not.Nullable();
        }
    }
}
