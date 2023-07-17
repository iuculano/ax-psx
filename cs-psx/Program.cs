using axPSX.CPU;
using axPSX.System;

namespace axPSX
{
    class Program
    {
        static void Main(string[] args)
        {
            var memory    = new MemoryBus();
            var processor = new Processor(memory);

            while (true)
            {
                processor.Update();
            }
        }
    }
}
