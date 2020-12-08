using NHibernate;

namespace Byndyusoft.Data.Relational
{
    public interface INhSessionFactory
    {
        ISession CreateSession();
    }
}