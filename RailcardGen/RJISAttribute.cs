namespace RailcardGen
{
    class RJISAttribute : System.Attribute
    {
        public RJISTypes Type { get; private set; }
        public int StartColumn { get; private set; }
        public int EndColumn { get; private set; }

        public RJISAttribute(RJISTypes rjisType, int startColumn, int endColumn = -1)
        {
            Type = rjisType;
            StartColumn = startColumn;
            EndColumn = endColumn;
        }
    }
}