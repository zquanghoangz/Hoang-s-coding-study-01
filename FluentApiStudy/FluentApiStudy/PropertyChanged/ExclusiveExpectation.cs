using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FluentApiStudy.PropertyChanged
{
    public sealed class ExclusiveExpectation<T>
        where T : INotifyPropertyChanged
    {
        private readonly T _subject;
        private readonly IEnumerable<string> _expectedProps;

        public ExclusiveExpectation(T subject, IEnumerable<string> expectedProps)
        {
            _subject = subject;
            _expectedProps = expectedProps;
        }

        public void When(Action action)
        {
            var notifications = new List<string>();
            _subject.PropertyChanged += (o, e) => notifications.Add(e.PropertyName);

            action();

            var unexpected = notifications.Except(_expectedProps);

            Assert.Fail($"Received notifications: {FormatNames(notifications)}." +
                $" Expected {FormatNames(_expectedProps)} and nothing else");
        }

        private string FormatNames(IEnumerable<string> propertyNames)
        {
            return string.Join(", ", propertyNames.Select(x => $"{typeof(T).Name}.{x}"));
        }
    }
}