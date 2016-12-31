using System.Collections.Generic;
using System.Text;

namespace DotNet.SourceMaps
{


    public class SourceMap
    {
        public SourceMap()
        {
            Sources = new List<string>();
            Names = new List<string>();
            Version = 3;
            SourcesContent = new List<string>();
        }

        public int Version { get; set; }

        public string SourceRoot { get; set; }

        public List<string> Sources { get; set; }

        public List<string> SourcesContent { get; set; }

        public List<string> Names { get; set; }

        public string Mappings { get; set; }

        public string File { get; set; }

        //public string ToJson()
        //{
        //    var builder = new StringBuilder();
        //    builder.Append("{");

        //    builder.Append("{");


        //}


    }
}