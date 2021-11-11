using System;

namespace HackAssembler
{
    static class Utils
    {
        public static string ToBinary(int number)
        {
            return Convert.ToString(number, 2).PadLeft(16, '0');
        }

        public static string ToBinary(string symbol)
        {
            int.TryParse(symbol, out int address);

            return ToBinary(address);
        }
    }
}
