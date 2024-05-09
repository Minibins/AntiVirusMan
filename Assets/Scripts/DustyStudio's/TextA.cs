using System;
using System.Text;

namespace DustyStudios.TextAVM
{
    public static class TextA
    {
        public static string Stretch(string input,float factor)
        {
            if(factor <= 0||input=="")
                return "";
            factor = 1f / factor;
            StringBuilder result = new();
            for(float s = 0; s < input.Length; s += factor)
                result.Append(input[(int)s]);
            return result.ToString();
        }
    }
}
