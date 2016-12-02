using FluentApiStudy.PropertyChanged;
using NUnit.Framework;
using System;

namespace FluentApiStudy.UnitTest
{
    [TestFixture]
    public sealed class BasicTest
    {
        [Test]
        public void SingleProperty_AssertFail()
        {
            var person = new Person();

            var ex = Assert.Throws<AssertionException>(() =>
            {
                person.ShouldNotifyFor(x => x.FirstName)
                    .When(() => {/*Do nothing*/});
            });

            Assert.That(
                ex.Message,
                Is.EqualTo("Received notifications: (none). " +
                "Expected Person.FirstName."));
        }

        [Test]
        public void SingleProperty_AssertPass()
        {
            var person = new Person();
            person.ShouldNotifyFor(x => x.FirstName)
                .When(() => person.FirstName = "Ryan");
        }

        // TODO: if the expression isn't a simple property access
        [Test]
        public void NonPropertyExpression_Exception()
        {
            var person = new Person();

            var ex = Assert.Throws<ArgumentException>(() =>
                person.ShouldNotifyFor(x => "not a property expression")
            );

            Assert.That(
                ex.Message,
                Is.EqualTo("Expression must be a simple property access of the form \"x => x.PropertyName\"."));
        }

        [Test]
        public void MultipleProperties_AssertFail()
        {
            var person = new Person();

            var ex = Assert.Throws<AssertionException>(() =>
            {
                person.ShouldNotifyFor(x => x.FirstName)
                .And(x => x.LastName)
                .When(() => person.FirstName = "Ryan");
            });

            var expectedMessage = "Received notifications: Person.FirstName, Person.FullName. Expected Person.FirstName, Person.LastName.";

            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void MultipleProperties_AssertPass()
        {
            var person = new Person();

            person.ShouldNotifyFor(x => x.FirstName)
            .And(x => x.FullName)
            .When(() =>
            {
                person.FirstName = "Ryan";
                //person.LastName = "Tong";
            });
        }

        [Test]
        public void MultipleProperties_NoNotifications_AssertFail()
        {
            var person = new Person();

            var ex = Assert.Throws<AssertionException>(() =>
            {
                person.ShouldNotifyFor(x => x.FirstName)
                .And(x => x.LastName)
                .When(() => {/*Nothing happens*/});
            });

            var expectedMessage = "Received notifications: (none). Expected Person.FirstName, Person.LastName.";

            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }
    }
}