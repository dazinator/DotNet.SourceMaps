namespace DotNet.SourceMaps
{



    public struct LineNumberAndPosition
    {
        public int LineNumber { get; set; }
        public int ColumnPosition { get; set; }

        public LineNumberAndPosition(int lineNumber, int columnPosition)
        {
            LineNumber = lineNumber;
            ColumnPosition = columnPosition;
        }

        public static LineNumberAndPosition operator -(LineNumberAndPosition c1, LineNumberAndPosition c2)
        {
            return new LineNumberAndPosition(c1.LineNumber - c2.LineNumber, c1.ColumnPosition - c2.ColumnPosition);
        }

     
        public static bool operator ==(LineNumberAndPosition x, LineNumberAndPosition y)
        {
            return (x.ColumnPosition == y.ColumnPosition) && (x.LineNumber == y.LineNumber);
        }

        public static bool operator !=(LineNumberAndPosition x, LineNumberAndPosition y)
        {
            return (x.ColumnPosition != y.ColumnPosition) || (x.LineNumber != y.LineNumber);
        }

        // override object.Equals
    }
}