using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Pulse2DataLayer.Models.Classes;

namespace Pulse2DataAcessLayer.Maps
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table("Role");
            Id(role => role.RoleId).GeneratedBy.Assigned().Column("RoleId");
            Map(role => role.Name).Column("Name").Not.Nullable();
            Map(role => role.Description).Column("Description").Not.Nullable();
        }
    }
}
