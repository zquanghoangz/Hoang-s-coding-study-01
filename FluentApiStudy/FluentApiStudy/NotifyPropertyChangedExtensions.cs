using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FluentApiStudy
{
    public static class NotifyPropertyChangedExtensions
    {
        public static PropertyChangedExpectation<T>
            ShouldNotifyFor<T, TProp>(
                this T subject,
                Expression<Func<T, TProp>> propertyExpression)
            where T : INotifyPropertyChanged
        {
            return new PropertyChangedExpectation<T>(subject, ExpressionUtilities.GetPropertyName(propertyExpression));
        }
    }
}