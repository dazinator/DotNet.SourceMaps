namespace DotNet.SourceMaps
{

    //    Output location Input Input location Character
    //Line 1, Column 0	Yoda_input.txt Line 1, Column 5	t
    //Line 1, Column 1	Yoda_input.txt Line 1, Column 6	h
    //Line 1, Column 2	Yoda_input.txt Line 1, Column 7	e
    //Line 1, Column 4	Yoda_input.txt Line 1, Column 9	f
    //Line 1, Column 5	Yoda_input.txt Line 1, Column 10	o
    //Line 1, Column 6	Yoda_input.txt Line 1, Column 11	r
    //Line 1, Column 7	Yoda_input.txt Line 1, Column 12	c
    //Line 1, Column 8	Yoda_input.txt Line 1, Column 13	e
    //Line 1, Column 10	Yoda_input.txt Line 1, Column 0	f
    //Line 1, Column 11	Yoda_input.txt Line 1, Column 1	e
    //Line 1, Column 12	Yoda_input.txt Line 1, Column 2	e
    //Line 1, Column 13	Yoda_input.txt Line 1, Column 3	l

    public class SourceFileContext
    {

        public SourceFileContext(string nameOrContent, bool isContents)
        {
            CurrentLineNumber = 0;
            CurrentColumnIndex = 0;
            IsContent = isContents;
            if (isContents)
            {
                Content = nameOrContent;
            }
            else
            {
                Name = nameOrContent;
            }
        }

        public int CurrentLineNumber { get; private set; }

        public int CurrentColumnIndex { get; private set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null)
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            var target = obj as SourceFileContext;
            if (target == null)
            {
                return false;
            }

            return (target.Content == this.Content) && (target.Name == this.Name);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            if (!IsContent)
            {
                return this.Name.GetHashCode();
            }

            return this.Content.GetHashCode();
        }
      
        public bool IsContent { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public SourceFileContext AdvanceLineNumber(int howMany = 1)
        {
            CurrentLineNumber = CurrentLineNumber + howMany;
            CurrentColumnIndex = 0;
            return this;
        }

        public SourceFileContext AdvanceColumnPosition(int howMany = 1)
        {
            CurrentColumnIndex = CurrentColumnIndex + howMany;        
            return this;
        }


    }
}