using System.Linq;
using System;
using System.Collections.Generic;

namespace BigBitMask.NET
{
    public class BitMask
    {
        private static readonly char[] alpha = new char[] {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f',
            'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_',
        };

        private static readonly IDictionary<char, byte> reverse = new Dictionary<char, byte> {
            {'A', 0x00}, {'B', 0x01}, {'C', 0x02}, {'D', 0x03}, {'E', 0x04}, {'F', 0x05}, {'G', 0x06}, {'H', 0x07}, {'I', 0x08}, {'J', 0x09}, {'K', 0x0A}, {'L', 0x0B}, {'M', 0x0C}, {'N', 0x0D}, {'O', 0x0E}, {'P', 0x0F},
            {'Q', 0x10}, {'R', 0x11}, {'S', 0x12}, {'T', 0x13}, {'U', 0x14}, {'V', 0x15}, {'W', 0x16}, {'X', 0x17}, {'Y', 0x18}, {'Z', 0x19}, {'a', 0x1A}, {'b', 0x1B}, {'c', 0x1C}, {'d', 0x1D}, {'e', 0x1E}, {'f', 0x1F},
            {'g', 0x20}, {'h', 0x21}, {'i', 0x22}, {'j', 0x23}, {'k', 0x24}, {'l', 0x25}, {'m', 0x26}, {'n', 0x27}, {'o', 0x28}, {'p', 0x29}, {'q', 0x2A}, {'r', 0x2B}, {'s', 0x2C}, {'t', 0x2D}, {'u', 0x2E}, {'v', 0x2F},
            {'w', 0x30}, {'x', 0x31}, {'y', 0x32}, {'z', 0x33}, {'0', 0x34}, {'1', 0x35}, {'2', 0x36}, {'3', 0x37}, {'4', 0x38}, {'5', 0x39}, {'6', 0x3A}, {'7', 0x3B}, {'8', 0x3C}, {'9', 0x3D}, {'-', 0x3E}, {'_', 0x3F},
        };

        private readonly IList<byte> blocks;

        public BitMask()
        {
            blocks = new List<byte>();
        }

        public BitMask(string mask)
        {
            blocks = mask.Select(block =>
            {
                if (reverse.TryGetValue(block, out var blockByte))
                    return blockByte;
                throw new FormatException($@"Unsupported token ""{block}""");
            }).ToList();
        }

        public bool this[int bit]
        {
            get
            {
                if (bit < 0)
                    throw new ArgumentOutOfRangeException(nameof(bit), "bit must be greater or equals to zero");

                var block = bit / 6;

                return blocks.Count <= block ? false : (blocks[block] >> (bit % 6) & 0x1) == 0x1;
            }
            set
            {
                if (bit < 0)
                    throw new ArgumentOutOfRangeException(nameof(bit), "bit must be greater or equals to zero");

                var block = bit / 6;
                bit %= 6;

                if (blocks.Count <= block)
                    for (var i = blocks.Count; i <= block; i++)
                        blocks.Add(0x0);

                blocks[block] = (byte)(value ? blocks[block] | 0x1 << bit : blocks[block] & ~(0x1 << bit));
            }
        }

        public override string ToString() => new string(blocks.Select(block => alpha[block]).ToArray()).TrimEnd('A');
    }
}
