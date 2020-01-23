using AllOverIt.Fixture;
using CoreGameEngine.Shapes;
using CoreGameEngine.Structs;
using FluentAssertions;
using Xunit;

namespace CoreGameEngine.xUnit.Tests.DrawTests
{
    public class ShapeTests : AoiFixtureBase
    {
        [Theory]
        [InlineData(@"u2 r 2 ")]
        [InlineData(@"c red u2 r 2 ")]
        [InlineData(@"c red n1 2 u2 r 2 ")]
        [InlineData(@"n1 2 c red u2 r 2 ")]
        public void ShapeValidationTests(string regex)
        {
            Invoking(() => Shape.New(regex, 'X', Point3D.Empty))
                .Should()
                .NotThrow();
        }
    }
}