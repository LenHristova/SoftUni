using System;
using BashSoft.Exceptions;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft.IO.Commands
{
    public abstract class Command
    {
        private string _input;
        private string[] _data;
        private readonly Tester _judge;
        private readonly StudentsRepository _repository;
        private readonly IOManager _inputOutputManager;

        protected Command(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager)
        {
            Input = input;
            Data = data;
            _judge = judge;
            _repository = repository;
            _inputOutputManager = inputOutputManager;
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

        protected Tester Judge => _judge;

        protected StudentsRepository Repository => _repository;

        protected IOManager InputOutputManager => _inputOutputManager;

        public abstract void Execute();
    }
}
