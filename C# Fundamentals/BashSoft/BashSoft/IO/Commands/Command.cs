using System;

using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public abstract class Command : IExecutable
    {
        private string _input;
        private string[] _data;

        protected Command(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.Input = input;
            this.Data = data;
            this.Judge = judge;
            this.Repository = repository;
            this.InputOutputManager = inputOutputManager;
        }

        protected string Input
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

        protected string[] Data
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

        protected IContentComparer Judge { get; }

        protected IDatabase Repository { get; }

        protected IDirectoryManager InputOutputManager { get; }

        public abstract void Execute();
    }
}
