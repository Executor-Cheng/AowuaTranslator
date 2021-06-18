using System;

namespace Aowua
{
    public static class AowuaTranslator
    {
        private const string INVALIDINPUTMESSAGE = "~呜嗷啊嗷嗷嗷~~~嗷啊嗷嗷~啊嗷嗷呜~呜啊啊嗷呜啊嗷呜呜呜嗷~呜嗷啊呜呜呜呜呜嗷~~啊嗷嗷~呜啊呜嗷~啊啊啊呜啊~呜嗷呜嗷啊~呜呜啊啊";

        private static ReadOnlySpan<char> Aowua => "嗷呜啊~";

        public static unsafe string Convert(string text)
        {
            return string.Create(4 + 8 * text.Length, text, static (span, text) =>
            {
                fixed (char* span1stPtr = span)
                fixed (char* aowuaPtr = Aowua)
                fixed (char* textPtr = text)
                {
                    char* spanPtr = span1stPtr;
                    *spanPtr++ = aowuaPtr[3];
                    *spanPtr++ = aowuaPtr[1];
                    *spanPtr++ = *aowuaPtr;
                    int offset = 0;
                    for (int i = 0; i < text.Length; i++)
                    {
                        ushort c = textPtr[i];
                        for (int b = 12; b >= 0; b -= 4)
                        {
                            int hex = (c >> b) + offset++ & 15;
                            *spanPtr++ = aowuaPtr[(uint)hex >> 2];
                            *spanPtr++ = aowuaPtr[hex & 3];
                        }
                    }
                    *spanPtr++ = aowuaPtr[2];
                }
            });
        }
        
        public static unsafe string ConvertBack(string aowua)
        {
            int length = aowua.Length - 4;
            fixed (char* aowuaPtr = aowua)
            {
                if (aowua.Length < 4 ||
                length % 8 != 0 ||
                aowuaPtr[aowua.Length - 1] != '啊' ||
                *aowuaPtr != '~' ||
                aowuaPtr[1] != '呜' ||
                aowuaPtr[2] != '嗷')
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
                    fixed (char* current1stPtr = span)
                    fixed (char* aowuaPtr = aowua)
                    {
                        char* currentPtr = current1stPtr;
                        uint offset = 0;
                        for (int i = 3; i < aowua.Length - 1;)
                        {
                            uint code = 0;
                            for (int j = 0; j < 4; j++)
                            {
                                code = code << 4 | ((mapChar(aowuaPtr[i++]) << 2 | mapChar(aowuaPtr[i++])) + offset--) & 15;
                            }
                            *currentPtr++ = (char)code;
                        }
                    }
                });
            }
        }
    }
}
