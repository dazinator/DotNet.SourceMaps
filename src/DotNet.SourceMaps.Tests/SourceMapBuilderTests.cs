using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DotNet.SourceMaps.Tests
{


    public class SourceMapBuilderTests
    {
        private ITestOutputHelper _output;


        public SourceMapBuilderTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Can_Build_Source_Map()
        {

            // The following creates a sourcemap that maps the source file "foo.js" with contents:
            // foo bar 
            // baz

            // to the generated output file: "foo.min.js" which contains: 
            // f b
            // ba

            var builder = new SourceMapBuilder();
            builder.WithOutputFile("foo.js.min") // the name of the generated file.
                   .WithSource("foo.js") // Sets the name of the source we are presently mapping to the generated file.
                   .MapName("foo") // both source and output files are are presently at line 0 position 0, and we are mapping "foo" in current source location to current output location which is "f"
                   .AdvanceSourceColumnPosition(4) // Advance the position in the source file by "foo " so we are positioned now at "bar"
                   .AdvanceOutputColumnPosition(2) // Advance the position in the output file by "f " so we are positioned now at "b" in the output file.
                   .MapName("bar") // Map present output location to "bar" at present source file location.
                   .AdvanceSourceLineNumber(1) // We have finished mapping line 1 on source and output, so advance line number of both.
                   .AdvanceOutputLineNumber(1)
                   .MapName("baz");


            var sourceMap = builder.Build();
            Assert.NotNull(sourceMap);

            var lines = sourceMap.Mappings.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(2, lines.Length);

            var line1Segments = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var line2segments = lines[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);



        }


    }
}
