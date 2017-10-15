using System;

namespace RailcardGen
{
    internal class RJISReadInfoAttribute : Attribute
    {
        readonly string _extension;
        readonly bool _canHaveDelta;
        readonly string _prefix;
        readonly int _prefixOffset;
        public RJISReadInfoAttribute(string extension, bool canHaveDelta, string prefix = null, int prefixOffSet = 0)
        {
            _extension = extension;
            _canHaveDelta = canHaveDelta;
            _prefix = prefix;
            _prefixOffset = prefixOffSet;
        }

        public string Extension => _extension;

        public bool CanHaveDelta => _canHaveDelta;

        public string Prefix => _prefix;

        public int PrefixOffset => _prefixOffset;
    }
}