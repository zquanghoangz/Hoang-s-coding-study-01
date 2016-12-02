using FluentApiStudy.Models;
using FluentApiStudy.PropertyChanged;
using NUnit.Framework;

namespace FluentApiStudy.PropertyChangedTest
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