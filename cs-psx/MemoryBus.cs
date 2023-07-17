using System;
using System.IO;
using System.Text;
using System.Diagnostics;


namespace axPSX.System
{
    public partial class MemoryBus
    {
        // https://psx-spx.consoledev.net/memorymap/
        public byte[] RAM          { get; init; }
        public byte[] Expansion1   { get; init; }
        public byte[] Scratchpad   { get; init; }
        public byte[] IOPorts      { get; init; }
        public byte[] Expansion2   { get; init; }
        public byte[] Expansion3   { get; init; }
        public byte[] BIOS         { get; init; }
        //public byte[] CacheControl { get; init; }


        // Helper to help detect the memory region
        private uint[] memoryRegionMask =
        {
            0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, // KUSEG - 0x00000000 -> 0x1FC00000
            0x7FFFFFFF,                                     // KSEG0 - 0x80000000 -> 0x9FC00000
            0x1FFFFFFF,                                     // KSEG1 - 0xA0000000 -> 0xBFC00000

            // KSEG2 - Don't really understand what's going on here??
            // 00000000000000000000000000000110
        };

        public MemoryBus()
        {
            RAM        = new byte[2048 * 1024];
            Expansion1 = new byte[8192 * 1024];
            Scratchpad = new byte[1    * 1024];
            IOPorts    = new byte[4    * 1024];
            Expansion2 = new byte[8    * 1024];
            Expansion3 = new byte[2048 * 1024];
            BIOS       = new byte[512  * 1024];

            File.ReadAllBytes("bios.bin").CopyTo(BIOS, 0);
        }

        public uint ReadWord(uint address)
        {
            uint memoryRegionIndex = address >> 29; // Get 3 most significant bits to determine region
            uint physicalAddress   = address & memoryRegionMask[memoryRegionIndex];

            uint data;
            switch (physicalAddress)
            {
                case var addr when (addr <= 0x1F000000):
                    data = BitConverter.ToUInt32(RAM, (int)addr - 0x1F000000);
                    break;

                case var addr when (addr <= 0x1F800000):
                    data = BitConverter.ToUInt32(Expansion1, (int)addr - 0x1F800000);
                    break;

                case var addr when (addr <= 0x1F801000):
                    data = BitConverter.ToUInt32(Scratchpad, (int)addr - 0x1F801000);
                    break;

                case var addr when (addr <= 0x1F802000):
                    data = BitConverter.ToUInt32(IOPorts, (int)addr - 0x1F802000);
                    break;

                case var addr when (addr <= 0x1F802000):
                    data = BitConverter.ToUInt32(Expansion2, (int)addr - 0x1F802000);
                    break;

                case var addr when (addr <= 0x1FA00000):
                    data = BitConverter.ToUInt32(Expansion3, (int)addr - 0x1FA00000);
                    break;

                case var addr when (addr <= 0x1FC00000):
                    data = BitConverter.ToUInt32(BIOS, (int)addr - 0x1FC00000);
                    break;

                default:
                    throw new Exception($"Illegal memory read: {address}");
            }

            return data;
        }
    }
}
