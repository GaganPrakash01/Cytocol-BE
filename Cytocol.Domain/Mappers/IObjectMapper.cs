using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Mappers
{
    public interface IObjectMapper
    {
        TTarget ConvertToTarget<TSource, TTarget>(TSource source) where TTarget : class where TSource : class;
        void MapToTarget<TSource, TTarget>(TSource source, TTarget target);

    }
}
