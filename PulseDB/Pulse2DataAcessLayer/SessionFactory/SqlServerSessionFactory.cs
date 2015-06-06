using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Pulse2DataAcessLayer.SessionFactory
{
    public class SqlServerSessionFactory
    {
        private static readonly object Locked = new object();
        private static volatile SqlServerSessionFactory _instance;
        private static ISessionFactory _sessionFactory;
        private static string _connectionString;

        private SqlServerSessionFactory()
        {
#if(DEBUG)
            _connectionString = ConfigurationManager.ConnectionStrings["Debug"].ConnectionString;
#else
            _connectionString = ConfigurationManager.ConnectionStrings["Release"].ConnectionString;
#endif
            _sessionFactory = CreateSessionFactory();
        }

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory;}
        }

        public static SqlServerSessionFactory Instance
        {
            get
            {
                lock (Locked)
                    return _instance ?? (_instance = new SqlServerSessionFactory());
            }
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return
                Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_connectionString))
                    .Mappings(map => map.FluentMappings.AddFromAssemblyOf<SqlServerSessionFactory>())
                    .BuildSessionFactory();
        }

       
    }
}
