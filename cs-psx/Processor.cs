using axPSX.System;

namespace axPSX.CPU
{
    public partial class Processor
    {
        internal MemoryBus      memory;
        internal Registers      registers;
        internal InstructionSet instructionSet;


        public Processor(MemoryBus memory)
        {
            this.memory         = memory;
            this.registers      = new Registers();
            this.instructionSet = new InstructionSet(this);

            registers.PC = 0xBFC00000; // Reset vector
        }

        public int Update()
        {  
            return StepInstruction();
        }

        public int StepInstruction()
        {
            var fetched = memory.ReadWord(registers.PC);
            var opcode  = new Opcode(fetched);


            var instruction = instructionSet.Opcodes[opcode.Value];
            var func = instruction.Function;
            registers.PC += 4;

            func.Invoke(opcode);
            return 0;
        }
    }
}
