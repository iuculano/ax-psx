namespace axPSX
{
    public struct Opcode
    {
        private uint value;

        public Opcode(uint opcode)
        {
            value = opcode;
        }

        // Decoding helpers
        public int Value
        {
            // 11111100000000000000000000000000
            get => (int)value >> 26;
        }

        public uint Source
        {
            // 00000011111000000000000000000000
            get => (value & 0x03E00000) >> 21;
        }

        public uint Target
        {
            // 00000000000111110000000000000000
            get => (value & 0x001F0000) >> 16;
        }

        public uint Destination
        {
            // 00000000000000001111100000000000
            get => (value & 0x0000F800) >> 11;
        }

        public uint Shift
        {
            // 00000000000000000000011111000000
            get => (value & 0x000007C0) >> 6;
        }

        public uint Immediate
        {
            // 0000000000000000111111111111111
            get => (value & 0x0000FFFF);
        }

        public uint Jump
        {
            // 00000011111111111111111111111111
            get => (value & 0x03FFFFFF);
        }
    }
}
