namespace DotNet.SourceMaps
{


    public class SourceMapping
    {
        public LineNumberAndPosition Output { get; set; }
        public LineNumberAndPosition Input { get; set; }
        public int SourceIndex { get; set; }
        public int NameIndex { get; set; }

        public SourceMappingOffset Offset { get; private set; }

        public void SetOffset(SourceMapping previous)
        {
            Offset = new SourceMappingOffset(previous, this);
        }



    }

    //public struct SourceMappingSegment
    //{
    //    public SourceMappingSegment(int outputColumnIndex, int sourceIndex, int inputLineNumber, int inputColumnIndex, int nameIndex)
    //    {
    //        OutputColumnIndex = outputColumnIndex;
    //        SourceIndex = sourceIndex;
    //        InputLineNumber = inputLineNumber;
    //        InputColumnIndex = inputColumnIndex;
    //        NameIndex = nameIndex;
    //    }

    //    public int OutputColumnIndex { get; set; }

    //    public int SourceIndex { get; set; }

    //    public int InputLineNumber { get; set; }

    //    public int InputColumnIndex { get; set; }

    //    public int NameIndex { get; set; }

    //    public void WriteAsVariableLengthQuantityBase64(StringBuilder builder)
    //    {
    //        builder.Append(Base64VariableLengthQuantityFormat.Encode(OutputColumnIndex));
    //        builder.Append(Base64VariableLengthQuantityFormat.Encode(SourceIndex));
    //        builder.Append(Base64VariableLengthQuantityFormat.Encode(InputLineNumber));
    //        builder.Append(Base64VariableLengthQuantityFormat.Encode(InputColumnIndex));
    //        builder.Append(Base64VariableLengthQuantityFormat.Encode(NameIndex));
    //    }

    //}
}