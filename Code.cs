﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAssembler
{
    public static class Code
    {
        public static string Dest(string dest)
        {
            var map = new Dictionary<string, string>
            {
                { "M",   "001" },
                { "D",   "010" },
                { "DM",  "011" },
                { "MD",  "011" },
                { "A",   "100" },
                { "AM",  "101" },
                { "AD",  "110" },
                { "ADM", "111" }
            };

            if (map.ContainsKey(dest ?? ""))
                return map[dest];

            return "000";
        }

        public static string Comp(string comp)
        {
            var map = new Dictionary<string, string>
            {
                { "0",   "0101010" },
                { "1",   "0111111" },
                { "-1",  "0111010" },
                { "D",   "0001100" },
                { "A",   "0110000" },
                { "M",   "1110000" },
                { "!D",  "0001101" },
                { "!A",  "0110001" },
                { "!M",  "1110001" },
                { "-D",  "0001111" },
                { "-A",  "0110011" },
                { "-M",  "1110011" },
                { "D+1", "0011111" },
                { "A+1", "0110111" },
                { "M+1", "1110111" },
                { "D-1", "0001110" },
                { "A-1", "0110010" },
                { "M-1", "1110010" },
                { "D+A", "0000010" },
                { "D+M", "1000010" },
                { "D-A", "0010011" },
                { "D-M", "1010011" },
                { "A-D", "0000111" },
                { "M-D", "1000111" },
                { "D&A", "0000000" },
                { "D&M", "1000000" },
                { "D|A", "0010101" },
                { "D|M", "1010101" }
            };

            if (map.ContainsKey(comp ?? ""))
                return map[comp];

            return null;
        }

        public static string Jump(string jump)
        {
            var map = new Dictionary<string, string>
            {
                { "JGT", "001" },
                { "JEQ", "010" },
                { "JGE", "011" },
                { "JLT", "100" },
                { "JNE", "101" },
                { "JLE", "110" },
                { "JMP", "111" }
            };

            if (map.ContainsKey(jump ?? ""))
                return map[jump];

            return "000";
        }
    }
}
