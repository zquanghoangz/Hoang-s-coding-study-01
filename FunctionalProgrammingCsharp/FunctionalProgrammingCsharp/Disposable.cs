using System;

namespace FunctionalProgrammingCsharp
{
    public static class Disposable
    {
        /// <summary>
        /// Netted disposable in a method
        /// </summary>
        /// <typeparam name="TDisposable"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="factory"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static TResult Using<TDisposable, TResult>(
            Func<TDisposable> factory,
            Func<TDisposable, TResult> map
            ) where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return map(disposable);
            }
        }

        /// <summary>
        /// Simple map source to a function
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static TResult Map<TSource, TResult>
            (this TSource @this, Func<TSource, TResult> fn) => fn(@this);

        /// <summary>
        /// Run a void method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        public static T Tee<T>(this T @this, Action<T> act)
        {
            act(@this);
            return @this;
        }

    }
}