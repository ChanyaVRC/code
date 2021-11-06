using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code
{
    internal static class EnumHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ToUInt64<TEnum>(TEnum value) where TEnum : struct, Enum
            => ToUInt64(Type.GetTypeCode(typeof(TEnum)), value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ToUInt64<TEnum>(TypeCode enumTypeCode, TEnum value) where TEnum : struct, Enum =>
            enumTypeCode switch
            {
                TypeCode.SByte => (ulong)Unsafe.As<TEnum, sbyte>(ref value),
                TypeCode.Byte => Unsafe.As<TEnum, byte>(ref value),
                TypeCode.Boolean => Unsafe.As<TEnum, bool>(ref value) ? 1UL : 0UL,
                TypeCode.Int16 => (ulong)Unsafe.As<TEnum, short>(ref value),
                TypeCode.UInt16 => Unsafe.As<TEnum, ushort>(ref value),
                TypeCode.Char => Unsafe.As<TEnum, char>(ref value),
                TypeCode.UInt32 => Unsafe.As<TEnum, uint>(ref value),
                TypeCode.Int32 => (ulong)Unsafe.As<TEnum, int>(ref value),
                TypeCode.UInt64 => Unsafe.As<TEnum, ulong>(ref value),
                TypeCode.Int64 => (ulong)Unsafe.As<TEnum, long>(ref value),
                _ => 0,
            };

    }
}
