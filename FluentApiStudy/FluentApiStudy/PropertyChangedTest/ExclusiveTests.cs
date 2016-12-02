using FluentApiStudy.Models;
using FluentApiStudy.PropertyChanged;
using NUnit.Framework;

namespace FluentApiStudy.PropertyChangedTest
{
    [TestFixture]
    public sealed class ExclusiveTests
    {
        [Test]
        public void SingleProperty_AssertFail()
        {
            var person = new Person();

            var ex = Assert.Throws<AssertionException>(() =>
                person.ShouldNotifyFor(x => x.FirstName)
                    .AndNothingElse()
                    .When(() => person.FirstName = "Ryan")
                );

            var msg = "Received notifications: Person.FirstName, Person.FullName." +
                " Expected Person.FirstName and nothing else";

            Assert.That(ex.Message, Is.EqualTo(msg));
        }

        [Test]
        public void SingleProperty_AssertPass()
        {
            var person = new Person();

            var ex = Assert.Throws<AssertionException>(() =>
                person.ShouldNotifyFor(x => x.FirstName)
                    .And(x => x.FullName)
                    .AndNothingElse()
                    .When(() => person.FirstName = "Ryan")
                );
        }
    }
}