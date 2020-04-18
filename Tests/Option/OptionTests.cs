using FluentAssertions;
using LanguageExt;
using NUnit.Framework;

namespace Tests.Option
{
    [TestFixture]
    public sealed class OptionTests
    {
        [Test]
        public void NullableType() 
        {
            int? nullableInt = null;
            //You can't mark all types as nullable.
            //string? nullableString = null;

            //You can access to nullable value. It fails in runtime, not in compile time.
            //nullableInt.Value.Should().Be(1);

            nullableInt.HasValue.Should().BeFalse();
        }

        [Test]
        public void OptionalTypesPublicMethods() 
        {
            //None -> No data
            //Some -> Data
            Option<string> optionalString = "1";

            //You can't get the value directly.
            //optionalString.Length().Should().Be(1);

            //Match can returns Unit(void) or the inner type of the Option.
            optionalString.Match(
                None: () => true.Should().BeFalse(),
                Some: value => value.Should().Be("1"));

            //You can return another string value but can't map to another type.
            string optionalValue = optionalString.Match(
                None: () => string.Empty,
                Some: value => value);
            optionalValue.Should().Be("1");

            optionalString.IsSome.Should().BeTrue();
            optionalString.IsNone.Should().BeFalse();

            //These methods returns Unit(void) cause you can't reuturn values 
            //if not handle both branchs.
            optionalString.IfSome(value => value.Should().Be("1"));
            optionalString.IfNone(() => true.Should().BeTrue());

            //When you map the type the result will be another optional type.
            Option<int> number = optionalString.Map(value => int.Parse(value));
            number.Match(
                None: () => true.Should().BeTrue(),
                Some: num => num.Should().Be(1));

            //You can convert optional types to null using unsafe methods.
            var possibleNullValue = optionalString.MatchUnsafe(
                None: () => null,
                Some: value => value);
            possibleNullValue.Should().Be("1");

            string posibleNullValue2 = optionalString.IfNoneUnsafe(() => null);
        }

        [Test]
        public void BindMethodsThatReturnsOptionalTypes() 
        {
            Option<string> fullName1 = GetFirstName().Match(
                None: () => Prelude.None,
                Some: firstName =>
                {
                    return GetLastName().Match(
                        None: () => Option<string>.None,
                        Some: lastName => $"{firstName} {lastName}");
                });

            Option<string> fullName2 = GetFirstName().Bind(firstname =>
                GetLastName().Bind<string>(lastName =>
                {
                    return $"{firstname} {lastName}";
                }));

            //This only works with C#. Query syntax.
            Option<string> fullName3 =
                from firstName in GetFirstName()
                from lastName in GetLastName()
                select $"{firstName} {lastName}";

            fullName1.IsSome.Should().BeTrue();
            fullName2.IsSome.Should().BeTrue();
            fullName3.IsSome.Should().BeTrue();

            Option<string> GetFirstName() 
            {
                return "Alvaro";
            }

            Option<string> GetLastName()
            {
                return "Gonzalez";
            }
        }
    }
}
