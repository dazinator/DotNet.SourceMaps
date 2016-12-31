using System;
using System.Text;

namespace DotNet.SourceMaps
{

    public class SourceMappingOffset
    {
        public SourceMappingOffset(SourceMapping fromMapping, SourceMapping currentMapping)
        {
            FromMapping = fromMapping;
            Current = currentMapping;

            if (fromMapping == null)
            {

                InputOffset = currentMapping.Input;
                OutputOffset = currentMapping.Output;
                NameIndexOffset = currentMapping.NameIndex;
                SourceIndexOffset = currentMapping.SourceIndex;
            }
            else
            {
                if (fromMapping.Input.LineNumber == currentMapping.Input.LineNumber)
                {
                    InputOffset = currentMapping.Input - fromMapping.Input;
                }
                else
                {
                    InputOffset = currentMapping.Input;
                }
                if (fromMapping.Output.LineNumber == currentMapping.Output.LineNumber)
                {
                    OutputOffset = currentMapping.Output - fromMapping.Output;
                }
                else
                {
                    OutputOffset = currentMapping.Output;
                }

                // not all mappings have a name index, search for previous mapping with name index and go relative to that.

                if (currentMapping.NameIndex == null)
                {
                    NameIndexOffset = null;
                }
                else if (FromMapping.NameIndex != null)
                {
                    NameIndexOffset = currentMapping.NameIndex.Value - fromMapping.NameIndex.Value;

                }
                else
                {
                    // need to get previous name index
                    SourceMapping previousNameMapping = null;
                    var offSet = FromMapping.Offset;
                    while (offSet != null)
                    {
                        if (offSet.NameIndexOffset != null)
                        {
                            previousNameMapping = offSet.FromMapping;
                            break;
                        }

                        offSet = offSet.FromMapping.Offset;
                    }
                    if (previousNameMapping != null)
                    {
                        NameIndexOffset = currentMapping.NameIndex.Value - previousNameMapping.NameIndex.Value;
                    }
                }

                SourceIndexOffset = currentMapping.SourceIndex - fromMapping.SourceIndex;
            }
        }

        public SourceMapping FromMapping { get; set; }

        public SourceMapping Current { get; set; }

        public LineNumberAndPosition InputOffset { get; set; }

        public LineNumberAndPosition OutputOffset { get; set; }

        public int? NameIndexOffset { get; set; }

        public int SourceIndexOffset { get; set; }

        public void WriteAsVariableLengthQuantityBase64(StringBuilder builder)
        {

            builder.Append(Base64VariableLengthQuantityFormat.Encode(OutputOffset.ColumnPosition));
            builder.Append(Base64VariableLengthQuantityFormat.Encode(SourceIndexOffset));
            builder.Append(Base64VariableLengthQuantityFormat.Encode(InputOffset.LineNumber));
            builder.Append(Base64VariableLengthQuantityFormat.Encode(InputOffset.ColumnPosition));
            if (NameIndexOffset != null)
            {
                builder.Append(Base64VariableLengthQuantityFormat.Encode(NameIndexOffset.Value));
            }



        }
    }
}