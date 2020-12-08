using NHibernate;

namespace Byndyusoft.Data.Relational
{
    public interface INhSessionAccessor
    {
        ISession Session { get; }
    }
}