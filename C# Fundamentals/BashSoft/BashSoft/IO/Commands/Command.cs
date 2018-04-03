using System;

using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public abstract class Command : IExecutable
    {
        private string _input;
        private string[] _data;

        protected Command(string input, string[] data)
        {
            this.Input = input;
            this.Data = data;
        }

        public string Input
        {
            get => _input;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                _input = value;
            }
        }

        public string[] Data
        {
            get => _data;
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }

                _data = value;
            }
        }

        public abstract void Execute();
    }
}
