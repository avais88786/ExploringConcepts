using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploringConcepts.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static string AppendHello(this string value)
        {
            return string.Concat("Hello ", value);
        }

        public static string AppendHello<T>(this T value)
        {
            if (typeof(T) == typeof(int)){
                return string.Concat("Integer Hello ",value);
            }
            else
                return string.Concat("Hello ", value);
        }

        public static string AppendHello<T,V>(this T value,V appendText)
        {
                return string.Concat("TV Hello ", value," ",Convert.ToString(appendText));
            
        }
    }
}
