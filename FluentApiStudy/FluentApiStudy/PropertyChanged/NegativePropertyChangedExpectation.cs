using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace FluentApiStudy.PropertyChanged
{
    public class NegativePropertyChangedExpectation<T> : PropertyChangedExpectationBase<T>
        where T : INotifyPropertyChanged
    {
        public NegativePropertyChangedExpectation(
            T subject,
            params string[] expectedProps)
            : base(subject, new string[0], expectedProps)
        {
        }

        public NegativePropertyChangedExpectation(
            T subject,
            IEnumerable<string> expected,
            IEnumerable<string> notExpected)
            : base(subject, expected, notExpected)
        {
        }

        public NegativePropertyChangedExpectation<T> Nor<TProp>(Expression<Func<T, TProp>> propertyExpression)
        {
            var newPropertyNames = _nonExpectedProps.Concat(new[]
            {
                ExpressionUtilities.GetPropertyName(propertyExpression)
            })
                .ToArray();

            return new NegativePropertyChangedExpectation<T>(
                _subject,
                _expectedProps,
                newPropertyNames);
        }
    }
}