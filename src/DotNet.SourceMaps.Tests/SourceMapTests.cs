using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DotNet.SourceMaps.Tests
{

    public class SourceMapTests
    {
        private ITestOutputHelper _output;


        public SourceMapTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Can_Convert_Source_Map_To_Base64()
        {

            var sourceMap = new SourceMap();
            sourceMap.File = "somefile.js";
            sourceMap.Names.Add("foo");
            sourceMap.Names.Add("bar");

            sourceMap.SourceRoot = "/some/root";
            sourceMap.SourcesContent.Add("var x = 'test';");
            sourceMap.Sources.Add("foo.js");
            sourceMap.Mappings = "ABCDE,FGHIJ;ABCDE";

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var json = JsonConvert.SerializeObject(sourceMap, settings);


        }


    }
}