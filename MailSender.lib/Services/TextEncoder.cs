using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MailSender.Services
{
    public static class TextEncoder
    {
        public static string Encode(string str, int shift = 1)
        {
            return new string(str.Select(c => (char)(c + shift)).ToArray());
        }
        public static string Decode(string str, int shift = 1)
        {
            return Encode(str, -shift);
        }
    }
}
