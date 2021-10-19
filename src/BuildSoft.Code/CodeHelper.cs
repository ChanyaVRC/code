using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code
{
    internal class CodeHelper
    {
        private const int DefaultTabSize = 4;
        internal static int TabSize { get; set; } = DefaultTabSize;

        private static readonly Dictionary<int, string> _spaceCache = new()
        {
            { 1 * DefaultTabSize, new(' ', 1 * DefaultTabSize) },
            { 2 * DefaultTabSize, new(' ', 2 * DefaultTabSize) },
            { 3 * DefaultTabSize, new(' ', 3 * DefaultTabSize) },
            { 4 * DefaultTabSize, new(' ', 4 * DefaultTabSize) },
            { 5 * DefaultTabSize, new(' ', 5 * DefaultTabSize) },
            { 6 * DefaultTabSize, new(' ', 6 * DefaultTabSize) },
            { 7 * DefaultTabSize, new(' ', 7 * DefaultTabSize) },
            { 8 * DefaultTabSize, new(' ', 8 * DefaultTabSize) },
            { 9 * DefaultTabSize, new(' ', 9 * DefaultTabSize) },
            { 10 * DefaultTabSize, new(' ', 10 * DefaultTabSize) },
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CreateOrGetIndent(int indent)
        {
            int currentTabSize = TabSize;
            int key = checked(indent * currentTabSize);

            if (key == 0)
            {
                return string.Empty;
            }

            string outValue;
            if (_spaceCache.TryGetValue(key, out outValue!))
            {
                return outValue;
            }
            lock (_spaceCache)
            {
                if (_spaceCache.TryGetValue(key, out outValue!))
                {
                    return outValue;
                }
                string indentContent = new(' ', key);
                _spaceCache.Add(key, indentContent);
                return indentContent;
            }
        }
    }
}
