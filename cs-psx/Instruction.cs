namespace axPSX
{
    public class Instruction
    {
        public Instruction(Action<Opcode> function)
        {
            Function = function;
        }

        public Action<Opcode> Function
        {
            get; init;
        }
    }
}
