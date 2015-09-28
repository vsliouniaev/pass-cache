using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassCacheTray.Util
{
    public static class Extensions
    {
        public static bool IsLetter(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.Length == 1 && char.IsLetter(input[0]);
        }
    }
}
