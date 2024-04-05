#if NET6_0_OR_GREATER

using System;
using System.Data;
using Dapper;

namespace Byndyusoft.Data.Relational.TypeHandlers
{
    public class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
    {
        public override TimeOnly Parse(object value)
        {
            return value switch
            {
                TimeOnly time => time,
                DateTime date => TimeOnly.FromDateTime(date),
                TimeSpan span => TimeOnly.FromTimeSpan(span),
                _ => default
            };
        }

        public override void SetValue(IDbDataParameter parameter, TimeOnly value)
        {
            parameter.DbType = DbType.Time;
            parameter.Value = value;
        }
    }
}
#endif