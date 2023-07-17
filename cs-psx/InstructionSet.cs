using System.Diagnostics;

namespace axPSX.CPU
{
    public partial class InstructionSet
    {
        private Processor processor
        {
            get; init;
        }

        public List<Instruction> Opcodes = new List<Instruction>();

        public InstructionSet(Processor processor)
        {
            this.processor = processor;
            Initialize();
        }

        private void Initialize()
        {
            Opcodes = new List<Instruction>
            {
                new Instruction(OP_INVALID), // 0x00
                new Instruction(OP_INVALID), // 0x01
                new Instruction(OP_INVALID), // 0x02
                new Instruction(OP_INVALID), // 0x03
                new Instruction(OP_INVALID), // 0x04
                new Instruction(OP_INVALID), // 0x05
                new Instruction(OP_INVALID), // 0x06
                new Instruction(OP_INVALID), // 0x07
                new Instruction(OP_INVALID), // 0x08
                new Instruction(OP_INVALID), // 0x09
                new Instruction(OP_INVALID), // 0x0A
                new Instruction(OP_INVALID), // 0x0B
                new Instruction(OP_INVALID), // 0x0C
                new Instruction(OP_INVALID), // 0x0D
                new Instruction(OP_INVALID), // 0x0E
                new Instruction(OP_LUI),     // 0x0F
            };
        }


        private void OP_INVALID(Opcode opcode)
        {
            string str = string.Concat(
                "!! CPU FAULT - HERE BE DRAGONS !!\n",
                "This is likely:\n",
                "- An emulation bug\n",
                "- An unimplemented opcode\n\n",

                "Processor state is likely corrupt."
            );

            Console.WriteLine(str);
            Debugger.Break();
        }

        private void OP_LUI(Opcode opcode)
        {
            processor.registers.Index[opcode.Target] = opcode.Immediate << 16;
        }
    }
}
