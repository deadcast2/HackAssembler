using System.Collections.Generic;

namespace HackAssembler
{
    class Symbol
    {
        private Dictionary<string, string> _Map;

        private int _NextAvailableAddress = 16;
        private int _LocatedLCommands = 0;

        public Symbol()
        {
            _Map = new Dictionary<string, string>
            {
                { "SCREEN", Utils.ToBinary(16384) },
                { "KBD", Utils.ToBinary(24576) },
                { "R0", Utils.ToBinary(0) },
                { "R1", Utils.ToBinary(1) },
                { "R2", Utils.ToBinary(2) },
                { "R3", Utils.ToBinary(3) },
                { "R4", Utils.ToBinary(4) },
                { "R5", Utils.ToBinary(5) },
                { "R6", Utils.ToBinary(6) },
                { "R7", Utils.ToBinary(7) },
                { "R8", Utils.ToBinary(8) },
                { "R9", Utils.ToBinary(9) },
                { "R10", Utils.ToBinary(10) },
                { "R11", Utils.ToBinary(11) },
                { "R12", Utils.ToBinary(12) },
                { "R13", Utils.ToBinary(13) },
                { "R14", Utils.ToBinary(14) },
                { "R15", Utils.ToBinary(15) },
                { "SP", Utils.ToBinary(0) },
                { "LCL", Utils.ToBinary(1) },
                { "ARG", Utils.ToBinary(2) },
                { "THIS", Utils.ToBinary(3) },
                { "THAT", Utils.ToBinary(4) },
            };
        }

        public void AddEntry(string label, int lineNumber)
        {
            if (!_Map.ContainsKey(label))
            {
                _Map[label] = Utils.ToBinary(lineNumber - _LocatedLCommands++);
            }
        }

        public string GetEntry(string address)
        {
            if (_Map.ContainsKey(address))
            {
                return _Map[address];
            }
            else
            {
                if (int.TryParse(address, out int number))
                {
                    return _Map[address] = Utils.ToBinary(number);
                }

                return _Map[address] = Utils.ToBinary(_NextAvailableAddress++);
            }
        }
    }
}
