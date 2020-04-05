using FluentAssertions;
using LanguageExt;
using NUnit.Framework;

namespace Tests.Option
{
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void OptionalType()
        {
            Option<string> stringWithValue = "foo";
            Option<string> stringWithoutValue = null;

            stringWithValue.IsSome.Should().BeTrue();
            stringWithoutValue.IsNone.Should().BeTrue();

            stringWithValue.IfSome(value => value.Should().Be("foo"));

            //You can't access to any public method of strig directly.
            //stringWithValue.Length().Should().Be(3);

            //You must handle both branchs, none branch and some branch.
            stringWithValue.Match(
                None: () => true.Should().BeFalse(),
                Some: value => value.Length().Should().Be(3));
        }

        //This test breaks witn NullReferenceException
        //[Test]
        public void DifferenceBetweenNullableTypeAndOptionalType() 
        {
            int? nullableInt = null;
            Option<int> optionalInt = Prelude.None;

            //NullReferenceException
            nullableInt.Value.Should().Be(1);
            optionalInt.IfSome(number => number.Should().Be(1));
        }
    }
}
