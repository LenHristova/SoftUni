using System;

namespace P01_Stealer
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SoftUniAttribute : Attribute
    {
        public SoftUniAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}