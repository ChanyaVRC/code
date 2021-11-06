using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuildSoft.Code.Internal;

namespace BuildSoft.Code
{
    internal class ThrowHelper
    {
        [DoesNotReturn]
        public static void ThrowInvalidOperationException()
            => throw new InvalidOperationException();

        public static void ThrowInvalidOperationExceptionIf([DoesNotReturnIf(true)] bool conditions)
        {
            if (conditions)
            {
                throw new InvalidOperationException();
            }
        }

        public static void ThrowIOExceptionIfNullOrCantWrite([NotNull] Stream stream)
        {
            if (stream == null || !stream.CanWrite)
            {
                throw new IOException();
            }
        }

        public static void ThrowArgumentOutOfRangeExceptionIfNegative(int value, ParamName paramName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName.GetParamName());
            }
        }

    }
}
