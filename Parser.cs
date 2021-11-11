using System;
using System.Collections.Generic;
using System.IO;

namespace HackAssembler
{
    class Parser
    {
        public enum CommandType { A_COMMAND, C_COMMAND, L_COMMAND, UNKNOWN }

        private Symbol _Symbol = new();

        public Parser(string filepath)
        {
            try
            {
                var output = FirstPass(File.ReadAllLines(filepath));

                File.WriteAllLines(Path.ChangeExtension(filepath, "hack"), output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open file '{filepath}'. Error: {ex.Message}");
            }
        }

        private string[] FirstPass(string[] lines)
        {
            var cleanedLines = RemoveWhiteSpaceAndComments(lines);

            for (var i = 0; i < cleanedLines.Length; i++)
            {
                switch (GetCommandType(cleanedLines[i]))
                {
                    case CommandType.L_COMMAND:
                        _Symbol.AddLEntry(GetSymbol(cleanedLines[i]), i);
                        break;
                }
            }

            return SecondPass(cleanedLines);
        }

        private string[] SecondPass(string[] lines)
        {
            var binaryLines = new List<string>();

            for (var i = 0; i < lines.Length; i++)
            {
                switch(GetCommandType(lines[i]))
                {
                    case CommandType.A_COMMAND:
                        binaryLines.Add(_Symbol.GetEntry(GetSymbol(lines[i])));
                        break;
                    case CommandType.C_COMMAND:
                        var destCode = Code.Dest(GetDest(lines[i]));
                        var compCode = Code.Comp(GetComp(lines[i]));
                        var jumpCode = Code.Jump(GetJump(lines[i]));
                        binaryLines.Add($"111{compCode}{destCode}{jumpCode}");
                        break;
                }
            }

            return binaryLines.ToArray();
        }

        private string GetSymbol(string command)
        {
            return command.Replace("@", "").Replace("(", "").Replace(")", "");
        }

        private string GetDest(string command)
        {
            var groups = command.Split("=");

            if (groups.Length > 1) return groups[0];

            return null;
        }

        private string GetComp(string command)
        {
            var groups = command.Split(";")[0].Split("=");

            if (groups.Length > 1) return groups[1];

            return groups[0];
        }

        private string GetJump(string command)
        {
            var groups = command.Split(";");

            if (groups.Length > 1) return groups[1];

            return null;
        }

        private string[] RemoveWhiteSpaceAndComments(string[] lines)
        {
            var cleanedLines = new List<string>();

            foreach (var line in lines)
            {
                string modifiedLine = RemoveSpaces(RemoveComments(line));

                if (string.IsNullOrWhiteSpace(modifiedLine))
                    continue;

                cleanedLines.Add(modifiedLine.Trim());
            }

            return cleanedLines.ToArray();
        }

        private string RemoveSpaces(string line)
        {
            return line.Replace(" ", "");
        }

        private string RemoveComments(string line)
        {
            var commentStart = line.IndexOf("//");

            if (commentStart >= 0)
                line = line.Remove(commentStart);

            return line;
        }

        private CommandType GetCommandType(string line)
        {
            if (line.StartsWith('@'))
                return CommandType.A_COMMAND;
            else if (line.Contains("=") || line.Contains(";"))
                return CommandType.C_COMMAND;
            else if (line.StartsWith("("))
                return CommandType.L_COMMAND;

            return CommandType.UNKNOWN;
        }
    }
}
