using System;
using System.Linq;
using System.Linq.Expressions;

namespace FluentMapper
{
    public class TypeMappingSpect<TTarget, TSource>
    {
        public IMapper<TTarget, TSource> Create()
        {
            return new Mapper();
        }

        public interface IMapper<T, T1>
        {
        }

        public sealed class Mapper : IMapper<TTarget, TSource>
        {
            public TTarget Map(TSource source)
            {
                var targetProperties = typeof(TTarget).GetProperties();
                var sourceProperties = typeof(TSource).GetProperties();
                foreach (var targetProperty in targetProperties)
                {
                    var sourceProperty = sourceProperties
                        .First(x => x.Name == targetProperty.Name);

                    var srcParam = Expression.Parameter(typeof(TSource));
                    var tgtParam = Expression.Parameter(typeof(TTarget));
                    var setter = targetProperty.GetSetMethod();
                    var getterExpression = Expression.Property(srcParam, sourceProperty);
                    var setterCallExpression = Expression.Call(tgtParam, setter, getterExpression);

                    //var lambda = Expression.Lambda<Action<TTarget, TSource>>(setterCallExpression, );

                    //actions.Add(lambda.Compile());
                }

                var target = (TTarget)Activator.CreateInstance(typeof(TTarget));

                //actions

                return target;
            }




        }
    }

    public static class FluentMapper
    {
        public static TargetTypeSpect<TTarget> ThatMaps<TTarget>(this TTarget target)
        {
            return new TargetTypeSpect<TTarget>();
        }
    }

    public class TargetTypeSpect<TTarget>
    {

    }
}