using System;

namespace BashSoft.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AliasAttribute : Attribute
    {
        private readonly string _name;

        public AliasAttribute(string name)
        {
            _name = name;
        }

        public string Name => _name;

        public override bool Equals(object obj)
        {
            return _name.Equals(obj);
        }
    }
}
