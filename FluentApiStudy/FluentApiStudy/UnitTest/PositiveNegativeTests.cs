using FluentApiStudy.PropertyChanged;
using NUnit.Framework;
using System;

namespace FluentApiStudy.UnitTest
{
    [TestFixture]
    public sealed class PositiveNegativeTests
    {
        [Test]
        public void PositivePlusNegative_AssertFailed()
        {
            var person = new Person();

            var ex = Assert.Throws<AssertionException>(() =>
            {
                person.ShouldNotifyFor(x => x.FirstName)
                    .ButNot(x => x.FullName)
                    .When(() => person.FirstName = "Ryan");
            });

            var expectatedMessage = "Received notifications: Person.FirstName, Person.FullName. " +
                                    "Expected Person.FirstName but not Person.FullName.";

            Assert.That(ex.Message, Is.EqualTo(expectatedMessage));
        }

        [Test]
        public void PositivePlusNegative_AssertPass()
        {
            var person = new Person();
            person.ShouldNotifyFor(x => x.FirstName)
                   .ButNot(x => x.LastName)
                   .When(() => person.FirstName = "Ryan");
        }

        [Test]
        public void PositivePlusMiltipleNagative_AssertFail()
        {
            var person = new Person();

            var ex = Assert.Throws<AssertionException>(() =>
            {
                person.ShouldNotifyFor(x => x.FirstName)
                    .ButNot(x => x.LastName)
                    .Nor(x => x.FullName)
                    .When(() =>
                    {
                        person.FirstName = "Ryan";
                        person.LastName = "Tong";
                    });
            });

            var expectatedMessage = "Received notifications: Person.FirstName, Person.FullName, Person.LastName, Person.FullName. " +
                                   "Expected Person.FirstName but not Person.LastName, Person.FullName.";

            Assert.That(ex.Message, Is.EqualTo(expectatedMessage));
        }

        [Test]
        public void PositivePlusMiltipleNagative_AssertPass()
        {
            var person = new Person();
            person.ShouldNotifyFor(x => x.BirthDate)
                 .ButNot(x => x.FirstName)
                 .Nor(x => x.LastName)
                 .When(() =>
                 {
                     person.BirthDate = DateTime.Now;
                 });
        }
    }
}