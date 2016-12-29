using System.Collections.Generic;
using System.Text;

namespace DotNet.SourceMaps
{

    public class SourceMappingVlqFormatter
    {
        public static void Format(StringBuilder builder, List<SourceMapping> mappings)
        {
            SourceMapping previous = null;
            //  bool isFirst = false;

            for (int i = 0; i < mappings.Count; i++)
            {
                var mapping = mappings[i];

                bool isLineAdvance = false;
                if (previous == null)
                {

                }
                else
                {
                    isLineAdvance = mapping.Output.LineNumber > previous.Output.LineNumber;
                }


                if (i != 0)
                {
                    if (isLineAdvance)
                    {
                        builder.Append(';');
                    }
                    else
                    {
                        builder.Append(',');
                    }
                }

                previous = mapping;

                mapping.Offset.WriteAsVariableLengthQuantityBase64(builder);
            }

        }
    }
}