namespace axPSX
{
    public struct Registers
    {
        public uint[] Index;
        public uint   PC;
        public uint   Hi;
        public uint   Lo;

        public Registers()
        {
            Index = new uint[32];
        }
    }
}
