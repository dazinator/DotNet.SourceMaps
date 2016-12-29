using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DotNet.SourceMaps.Tests
{

    public class SourceMappingOffsetTests
    {
        private ITestOutputHelper _output;


        public SourceMappingOffsetTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(new[] { 1, 2, 1, 7 }, new[] { 1, 2, 1, 7 })]
        public async Task Can_Create_Offset(int[] sourceMappingFrom, int[] sourceMapping)

        //   int inputLineNumberMappingTwo, int inputColumnMappingTwo, int outputLineNumberMappingTwo, int outputColumnMappingTwo)
        {

            int inputLineNumber = sourceMappingFrom[0];
            int inputColumn = sourceMappingFrom[1];
            int outputLineNumber = sourceMappingFrom[2];
            int outputColumn = sourceMappingFrom[3];

            var input = new LineNumberAndPosition() { LineNumber = inputLineNumber, ColumnPosition = inputColumn };
            var output = new LineNumberAndPosition() { LineNumber = outputLineNumber, ColumnPosition = outputLineNumber };

            var mapFrom = new SourceMapping() { Input = input, Output = output, NameIndex = 0, SourceIndex = 0 };

            inputLineNumber = sourceMapping[0];
            inputColumn = sourceMapping[1];
            outputLineNumber = sourceMapping[2];
            outputColumn = sourceMapping[3];

            input = new LineNumberAndPosition() { LineNumber = inputLineNumber, ColumnPosition = inputColumn };
            output = new LineNumberAndPosition() { LineNumber = outputLineNumber, ColumnPosition = outputLineNumber };

            var mapTo = new SourceMapping() { Input = input, Output = output, NameIndex = 0, SourceIndex = 0 };

            var sut = new SourceMappingOffset(mapFrom, mapTo);
         
            Assert.NotNull(sut);
        
            Assert.NotNull(sut.OutputOffset);
            Assert.Equal(mapTo.Output - mapFrom.Output, sut.OutputOffset);

            Assert.NotNull(sut.InputOffset);
            Assert.Equal(mapTo.Input - mapFrom.Input, sut.InputOffset);

            Assert.Equal(mapTo.NameIndex - mapFrom.NameIndex, sut.NameIndexOffset);
            Assert.Equal(mapTo.SourceIndex - mapFrom.SourceIndex, sut.SourceIndexOffset);

        }


    }
}