using FluentAssertions;
using LanguageExt;
using NUnit.Framework;

namespace Tests.Either
{
    [TestFixture]
    public sealed class EitherTests
    {
        [Test]
        public void EitherType()
        {
            Either<Error, string> successResult = "1";
            Either<Error, string> errorResult = Error.SomeError;

            successResult.IsRight.Should().BeTrue();
            successResult.IsLeft.Should().BeFalse();

            //These methods returns Unit(void) cause you can't reuturn values 
            //if not handle both branchs.
            successResult.IfRight(value => value.Should().Be("1"));
            errorResult.IfLeft(error => error.Should().Be(Error.SomeError));

            successResult.Match(
                Left: error => error.Should().Be(Error.SomeError),
                Right: value => value.Should().Be("1"));

            //When you map the type the result will be another either type.
            var errorMessage = errorResult.MapLeft(error => "stringErrorMessage");
            errorMessage.Match(
                Left: stringError => stringError.Should().Be("stringErrorMessage"),
                Right: result => true.Should().BeTrue());
        }

        private enum Error
        {
            SomeError,
            AnotherError
        }
    }
}