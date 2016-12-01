using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace FluentApiStudy
{
    public class PropertyChangedExpectation<T> where T : INotifyPropertyChanged
    {
        private readonly T _subject;
        private readonly string _propertyName;
        private readonly IEnumerable<string> _propertyNames;

        public PropertyChangedExpectation(T subject, params string[] propertyNames)
        {
            _subject = subject;
            _propertyName = propertyNames.First();
            _propertyNames = propertyNames;
        }

        public PropertyChangedExpectation<T> And<TProp>(Expression<Func<T, TProp>> propertyExpression)
        {
            var newPropertyNames = _propertyNames.Concat(new string[]
            {
                ExpressionUtilities.GetPropertyName(propertyExpression)
            })
            .ToArray();

            return new PropertyChangedExpectation<T>(
                _subject,
                newPropertyNames);
        }

        public void When(Action action)
        {
            var notifications = new List<string>();
            _subject.PropertyChanged += (s, e) => { notifications.Add(e.PropertyName); };
            action();

            var metExpectations = _propertyNames.Intersect(notifications)
                .ToArray();
            var unmetExpectations = _propertyNames.Where(x => !notifications.Contains(x))
                .ToArray();

            if (!unmetExpectations.Any())
            {
                return;
            }

            List<string> messages = new List<string>();

            if (unmetExpectations.Length > 0)
            {
                messages.Add($"Expected PropertyChanged event to fire for {FormatNames(unmetExpectations)}.");
            }

            if (metExpectations.Length > 0)
            {
                messages.Add($"Expectation met for {FormatNames(metExpectations)}.");
            }

            Assert.Fail(string.Join(" ", messages));
        }

        public string FormatNames(IEnumerable<string> propertyNames)
        {
            return string.Join(", ", propertyNames.Select(x => $"{typeof(T).Name}.{x}"));
        }
    }
}