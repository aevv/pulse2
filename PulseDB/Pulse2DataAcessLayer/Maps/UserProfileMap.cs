using FluentNHibernate.Mapping;
using Pulse2DataLayer.Models.Classes;

namespace Pulse2DataAcessLayer.Maps
{
    public class UserProfileMap : ClassMap<UserProfile>
    {
        public UserProfileMap()
        {
            Table("UserProfile");
            Id(userProfile => userProfile.UserProfileId).GeneratedBy.Assigned().Column("UserProfileId");
            Map(userProfile => userProfile.UserId).Column("UserId").Not.Nullable();
            Map(userProfile => userProfile.Firstname).Column("Firstname").Nullable();
            Map(userProfile => userProfile.Lastname).Column("Lastname").Nullable();
        }
    }
}
