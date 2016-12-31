using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.SourceMaps
{

    public class SourceMapBuilder
    {


        public SourceMapBuilder()
        {
            //MappingOffsets = new List<SourceMappingOffset>();
            Mappings = new List<SourceMapping>();
            Names = new List<string>();
            Sources = new List<SourceFileContext>();
            CurrentMapping = null;
            // OutputFileContext = new SourceFileContext();
        }

        public SourceMapping CurrentMapping { get; set; }

        public List<SourceMapping> Mappings { get; set; }     

        private List<string> Names { get; set; }

        private List<SourceFileContext> Sources { get; set; }

        public SourceFileContext OutputFileContext { get; set; }

        private int CurrentSourceIndex { get; set; }

        public SourceFileContext CurrentSourceFileContext { get; private set; }
            
        public SourceMapBuilder WithOutputFile(string name)
        {
            this.OutputFileContext = new SourceFileContext(name, false);
            return this;
        }

        public int AddSource(SourceFileContext source)
        {
            this.Sources.Add(source);
            return this.Sources.Count - 1;
        }

        public SourceMapBuilder WithSource(string sourceNameOrContents, bool isContents = false)
        {

            for (int i = 0; i < Sources.Count; i++)
            {
                var source = Sources[i];
                if (source.Name == sourceNameOrContents || source.Content == sourceNameOrContents)
                {
                    CurrentSourceIndex = i;
                    CurrentSourceFileContext = source;
                }
            }

            var sourceContext = new SourceFileContext(sourceNameOrContents, isContents);
            if (isContents)
            {
                sourceContext.Content = sourceNameOrContents;
            }
            else
            {
                sourceContext.Name = sourceNameOrContents;
            }

            var index = AddSource(sourceContext);
            CurrentSourceIndex = index;
            CurrentSourceFileContext = sourceContext;
            return this;

        }

        public int AddName(string name)
        {
            this.Names.Add(name);
            return this.Sources.Count - 1;
        }

        public int EnsureName(string name)
        {
            var index = Names.IndexOf(name);
            if (index == -1)
            {
                return AddName(name);
            }
            return index;
        }

        public void AddMapping(int? nameIndex = null)
        {

            var currentSourceIndex = CurrentSourceIndex;
            var sourceContext = CurrentSourceFileContext;

            if (nameIndex != null && nameIndex >= Names.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(nameIndex));
            }

            var mapping = new SourceMapping()
            {
                SourceIndex = currentSourceIndex,
                NameIndex = nameIndex,
                Input = new LineNumberAndPosition(sourceContext.CurrentLineNumber, sourceContext.CurrentColumnIndex),
                Output = new LineNumberAndPosition(OutputFileContext.CurrentLineNumber, OutputFileContext.CurrentColumnIndex)
            };

            //  var mapping = new SourceMapping() { SourceIndex = sourceIndex, NameIndex = nameIndex, Input = new LineNumberAndPosition(CurrentLineNumber, ) }

            mapping.SetOffset(CurrentMapping);
            CurrentMapping = mapping;
            Mappings.Add(mapping);
        }

        public SourceMap Build()
        {
            var builder = new StringBuilder();
            SourceMappingVlqFormatter.Format(builder, Mappings);
            var sourceMap = new SourceMap() { Mappings = builder.ToString(), Names = Names, Version = 3 };
            sourceMap.File = this.OutputFileContext.Name;

            foreach (var item in Sources)
            {
                if (!item.IsContent)
                {
                    sourceMap.Sources.Add(item.Name);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return sourceMap;

        }

        public SourceMapBuilder MapName(string name)
        {
            var nameIndex = EnsureName(name);
            AddMapping(nameIndex);
            return this;
        }

        public SourceMapBuilder AdvanceSourceLineNumber(int howManyLines = 1)
        {
            this.CurrentSourceFileContext.AdvanceLineNumber(howManyLines);
            return this;
        }

        public SourceMapBuilder AdvanceSourceColumnPosition(int howManyCharacters = 1)
        {
            this.CurrentSourceFileContext.AdvanceColumnPosition(howManyCharacters);
            return this;
        }

        public SourceMapBuilder AdvanceOutputLineNumber(int howManyLines = 1)
        {
            this.OutputFileContext.AdvanceLineNumber(howManyLines);
            return this;
        }

        public SourceMapBuilder AdvanceOutputColumnPosition(int howManyCharacters = 1)
        {
            this.OutputFileContext.AdvanceColumnPosition(howManyCharacters);
            return this;
        }
    }
}