using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Aowua
{
    public static class AowuaTranslator
    {
        private const string INVALIDINPUTMESSAGE = "~呜嗷啊嗷嗷嗷~~~嗷啊嗷嗷~啊嗷嗷呜~呜啊啊嗷呜啊嗷呜呜呜嗷~呜嗷啊呜呜呜呜呜嗷~~啊嗷嗷~呜啊呜嗷~啊啊啊呜啊~呜嗷呜嗷啊~呜呜啊啊";

        private static ReadOnlySpan<char> _Aowua => "嗷呜啊~";

        public static string Convert(string text)
        {
            return string.Create(4 + 8 * text.Length, text, static (span, text) =>
            {
                span[0] = _Aowua[3];
                span[1] = _Aowua[1];
                span[2] = _Aowua[0];
                int offset = 0, spanOffset = 2;
                ReadOnlySpan<char> textSpan = text;
                for (int i = 0; i < textSpan.Length; i++)
                {
                    ushort c = textSpan[i];
                    for (int b = 12; b >= 0; b -= 4)
                    {
                        int hex = (c >> b) + offset++ & 15;
                        span[++spanOffset] = _Aowua[(int)((uint)hex >> 2)];
                        span[++spanOffset] = _Aowua[hex & 3];
                    }
                }
                span[^1] = _Aowua[2];
            });
        }
        
        public static string ConvertBack(string aowua)
        {
            int length = aowua.Length - 4;
            ReadOnlySpan<char> span = aowua;
            ref char first = ref MemoryMarshal.GetReference(span);
            if (aowua.Length < 4 ||
                length % 8 != 0 ||
                Unsafe.Add(ref first, aowua.Length - 1) != '啊' ||
                first != '~' ||
                Unsafe.Add(ref first, 1) != '呜' ||
                Unsafe.Add(ref first, 2) != '嗷')
            {
                throw new ArgumentException(INVALIDINPUTMESSAGE);
            }
            return string.Create(length / 8, aowua, static (span, aowua) =>
            {
                static uint mapChar(char aowua)
                {
                    return aowua switch
                    {
                        '嗷' => 0,
                        '呜' => 1,
                        '啊' => 2,
                        '~' => 3,
                        _ => throw new ArgumentException(INVALIDINPUTMESSAGE)
                    };
                }
                ref char current = ref MemoryMarshal.GetReference(span);
                ref char aowuaSpanFirst = ref Unsafe.AsRef(in aowua.GetPinnableReference());
                uint offset = 0;
                for (int i = 3; i < aowua.Length - 1; )
                {
                    uint code = 0;
                    for (int b = i + 8; i < b; i++)
                    {
                        code = code << 4 | ((mapChar(Unsafe.Add(ref aowuaSpanFirst, i)) << 2 | mapChar(Unsafe.Add(ref aowuaSpanFirst, ++i))) + offset--) & 15;
                    }
                    current = (char)code;
                    current = ref Unsafe.Add(ref current, 1);
                }
            });
        }
    }
}
