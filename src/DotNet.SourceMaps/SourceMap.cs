using System.Collections.Generic;

namespace DotNet.SourceMaps
{


    public class SourceMap
    {
        public SourceMap()
        {
            Sources = new List<string>();
            Names = new List<string>();
            Version = 3;
        }

        public int Version { get; set; }

        public List<string> Sources { get; set; }

        public List<string> Names { get; set; }

        public string Mappings { get; set; }

        public string File { get; set; }


    }
}