using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public record class CsIdentifier
    {
        public string Value { get; }

        public CsIdentifier(string value)
        {
            if (value.Length <= 0)
            {
                throw new ArgumentException("Identifier must be at least one character.", nameof(value));
            }
            if (!char.IsLetter(value[0]))
            {
                throw new ArgumentException($"'{value[0]}' cannot be used at the beginning of the identifier.", nameof(value));
            }

            for (int i = 1; i < value.Length; i++)
            {
                if (!char.IsLetterOrDigit(value[i]))
                {
                    throw new ArgumentException($"'{value[0]}' cannot be used at an identifier.", nameof(value));
                }
            }
            Value = value;
        }

        public override string ToString() => Value;

        public static implicit operator string(CsIdentifier value) => value.Value;
        public static implicit operator CsIdentifier(string value) => new(value);
    }
}
