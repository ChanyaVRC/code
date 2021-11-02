using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Internal
{
    internal class OneElementEnumerator<T> : IEnumerator<T>
    {
        private bool _hasElement = true;
        private readonly T _value;

        public T Current => _value;

        object IEnumerator.Current => _value!;

        public OneElementEnumerator(T value)
        {
            _value = value;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            bool canMoveNext = _hasElement;
            _hasElement = false;
            return canMoveNext;
        }

        public void Reset()
        {
            _hasElement = true;
        }
    }
}
