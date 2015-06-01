using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Pulse2DataLayer.Models.Classes;

namespace Pulse2DataAcessLayer.Maps
{
    public class UserInRoleMap : ClassMap<UsersInRole>
    {
        public UserInRoleMap()
        {
            Table("UserInRole");
            CompositeId()
                .KeyProperty(userInRole => userInRole.UserId, "UserId")
                .KeyProperty(userInRole => userInRole.RoleId, "RoleId");
            References(userInRole => userInRole.User).Column("UserId").Not.LazyLoad();
            References(userInRole => userInRole.Role).Column("RoleId").Not.LazyLoad();
        }
    }
}
