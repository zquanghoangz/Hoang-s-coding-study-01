using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace FluentApiStudy.PropertyChanged
{
    public class PositivePropertyChangedExpectation<T> : PropertyChangedExpectationBase<T>
        where T : INotifyPropertyChanged
    {
        public PositivePropertyChangedExpectation(T subject, params string[] expectedProps)
            : base(subject, expectedProps, new string[0])
        {
        }

        private PositivePropertyChangedExpectation(
            T subject,
            IEnumerable<string> expected,
            IEnumerable<string> notExpected)
            : base(subject, expected, notExpected)
        {
        }

        public PositivePropertyChangedExpectation<T> And<TProp>(Expression<Func<T, TProp>> propertyExpression)
        {
            var newPropertyNames = _expectedProps.Concat(new string[]
            {
                ExpressionUtilities.GetPropertyName(propertyExpression)
            })
                .ToArray();

            return new PositivePropertyChangedExpectation<T>(
                _subject,
                newPropertyNames,
                _nonExpectedProps);
        }

        public NegativePropertyChangedExpectation<T> ButNot<TProp>(Expression<Func<T, TProp>> propertyExpression)
        {
            var newPropertyNames = _nonExpectedProps.Concat(new string[]
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