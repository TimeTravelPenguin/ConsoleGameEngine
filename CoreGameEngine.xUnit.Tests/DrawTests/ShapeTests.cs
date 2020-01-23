using CoreGameEngine.Shapes;
using CoreGameEngine.Structs;
using Xunit;

namespace CoreGameEngine.xUnit.Tests.DrawTests
{
    public class ShapeTests
    {
        [Theory]
        [InlineData(@"u2 r 2 ", new[] {"u2", "r 2"})]
        [InlineData(@"c red u2 r 2 ", new[] {"c red", "u2", "r 2"})]
        [InlineData(@"c red n1 2 u2 r 2 ", new[] {"c red", "n1 2", "u2", "r 2"})]
        [InlineData(@"n1 2 c red u2 r 2 ", new[] {"n1 2", "c red", "u2", "r 2"})]
        public void ShapeValidationTests(string regex, string[] expected)
        {
            var actual = Shape.New(regex, 'X', Point3D.Empty);

            //actual.Should()
            //  .BeEquivalentTo(expected);
        }
    }
}