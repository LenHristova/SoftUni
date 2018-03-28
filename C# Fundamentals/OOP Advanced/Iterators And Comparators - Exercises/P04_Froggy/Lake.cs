using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace P04_Froggy
{
    public class Lake<T> : IEnumerable<T>
    {
        private readonly T[] _stonesNumbers;

        public Lake(IEnumerable<T> stonesNumbers)
        {
            _stonesNumbers = stonesNumbers.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int index = 0; index < _stonesNumbers.Length; index+=2)
            {
                yield return _stonesNumbers[index];
            }

            var len = _stonesNumbers.Length;
            var start = len % 2 == 0
                ? len - 1
                : len - 2;

            for (int index = start; index >= 0; index-=2)
            {
                yield return _stonesNumbers[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}