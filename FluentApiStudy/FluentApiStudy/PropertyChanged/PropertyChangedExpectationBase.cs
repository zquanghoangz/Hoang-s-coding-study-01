using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FluentApiStudy.PropertyChanged
{
    public abstract class PropertyChangedExpectationBase<T> where T : INotifyPropertyChanged
    {
        protected readonly T _subject;
        protected readonly IEnumerable<string> _expectedProps;
        protected IEnumerable<string> _nonExpectedProps;

        protected PropertyChangedExpectationBase(T subject, params string[] expectedProps)
            : this(subject, expectedProps, new string[0])
        { }

        protected PropertyChangedExpectationBase(
            T subject,
            IEnumerable<string> expected,
            IEnumerable<string> notExpected)
        {
            _subject = subject;
            _expectedProps = expected;
            _nonExpectedProps = notExpected;

            var conflicts = _expectedProps.Intersect(_nonExpectedProps).ToArray();
            if (conflicts.Any())
            {
                throw new ArgumentException("Cannot specify properties for both positive and negative verification. " +
                      $"Conflicting properties: {FormatNames(conflicts)}");
            }
        }

        public void When(Action action)
        {
            var notifications = new List<string>();
            _subject.PropertyChanged += (s, e) => { notifications.Add(e.PropertyName); };
            action();

            var metExpectations = _expectedProps.Intersect(notifications)
                .ToArray();
            var unmetExpectations = _expectedProps.Where(x => !notifications.Contains(x))
                .ToArray();
            var unexpectedNotifications = _nonExpectedProps.Intersect(notifications).ToArray();

            if (!unmetExpectations.Any() && !unexpectedNotifications.Any())
            {
                return;
            }

            var receivedText = notifications.Any() ? FormatNames(notifications) : "(none)";

            var msg = $"Received notifications: {receivedText}. " +
                                    $"Expected {FormatNames(_expectedProps)}";

            if (_nonExpectedProps.Any())
            {
                msg += $" but not {FormatNames(_nonExpectedProps)}.";
            }
            else
            {
                msg += ".";
            }


            Assert.Fail(msg);
        }

        protected string FormatNames(IEnumerable<string> propertyNames)
        {
            return string.Join(", ", propertyNames.Select(x => $"{typeof(T).Name}.{x}"));
        }
    }
}