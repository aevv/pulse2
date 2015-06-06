using FluentNHibernate.Mapping;
using Pulse2DataLayer.Models.Classes;

namespace Pulse2DataAcessLayer.Maps
{
    public class SessionMap : ClassMap<Session>
    {
        public SessionMap()
        {
            Table("Session");
            CompositeId()
                .KeyProperty(session => session.UserId, "UserId")
                .KeyProperty(session => session.SessionId, "SessionId");
            References(session => session.UserId).Column("UserId").Not.LazyLoad().Cascade.SaveUpdate();
            Map(session => session.Token).Column("Token").Not.Nullable();
        }
    }
}
