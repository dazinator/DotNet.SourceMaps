using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DotNet.SourceMaps.Tests
{

    public class LineNumberAndPositionTests
    {
        private ITestOutputHelper _output;


        public LineNumberAndPositionTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Can_Subtract_Line_Number_And_Position()
        {

            var foo = new LineNumberAndPosition() { LineNumber = 1, ColumnPosition = 2 };
            var bar = new LineNumberAndPosition() { LineNumber = 1, ColumnPosition = 7 };

            var remainder = bar - foo;
            Assert.Equal(0, remainder.LineNumber);
            Assert.Equal(5, remainder.ColumnPosition);


        }


    }
}