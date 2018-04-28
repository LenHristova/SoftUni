using System;

namespace P01_Database
{
    public class Database<T>
    {
        private const int DEFAULT_CAPACITY = 16;

        protected T[] _values;
        protected int _currentIndex;

        protected Database(int valuesCount = 0)
        {
            EnsureValidValuesCount(valuesCount);
            _values = new T[16];
            _currentIndex = 0;
        }

        public Database(params T[] values) : this(values?.Length ?? 0)
        {
            Initialize(values);
        }

        protected void Initialize(params T[] values)
        {
            if (values == null)
            {
                return;
            }

            foreach (var value in values)
            {
                Add(value);
            }
        }

        protected static void EnsureValidValuesCount(int valuesCount)
        {
            if (valuesCount > DEFAULT_CAPACITY)
            {
                throw new InvalidOperationException("Values count can not be greater then 16.");
            }
        }

        public virtual void Add(T element)
        {
            if (_currentIndex == DEFAULT_CAPACITY)
            {
                throw new InvalidOperationException("Database is full.");
            }

            _values[_currentIndex] = element;
            _currentIndex++;
        }

        public void Remove()
        {
            if (_currentIndex == 0)
            {
                throw new InvalidOperationException("Databse is empty.");
            }

            _currentIndex--;
            _values[_currentIndex] = default(T);

        }

        public T[] Fetch()
        {
            var arrayToReturn = new T[_currentIndex];
            Array.Copy(_values, arrayToReturn, _currentIndex);
            return arrayToReturn;
        }
    }
}
