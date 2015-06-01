using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using Pulse2DataLayer.Models.Classes;

namespace Pulse2DataAcessLayer.Maps
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("User");
            Id(user => user.UserId).GeneratedBy.Assigned().Column("UserId");
            Map(user => user.Username).Column("Username").Not.Nullable();
            Map(user => user.LastActivityDate).Column("LastActivityDate").Not.Nullable();
            HasMany(user => user.Memberships).KeyColumn("UserId").Not.LazyLoad();
        }
    }
}
