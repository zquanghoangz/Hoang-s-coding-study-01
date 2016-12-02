using FluentApiStudy.PropertyChanged;
using NUnit.Framework;
using System;

namespace FluentApiStudy.UnitTest
{
    [TestFixture]
    public sealed class ArgumentValidationTests
    {
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
        public void PositiveNegative_Conflict()
        {
            var person = new Person();

            var ex = Assert.Throws<ArgumentException>(() =>
                person.ShouldNotifyFor(x => x.FirstName)
                    .ButNot(x => x.FirstName)
            );

            var msg = "Cannot specify properties for both positive and negative verification. " +
                      "Conflicting properties: Person.FirstName";

            Assert.That(ex.Message, Is.EqualTo(msg));
        }
    }
}