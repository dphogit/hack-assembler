namespace Assembler.AsmCommands;

public interface IAsmCommand
{
    public string Translate();
}

// A-Instruction: @Xxx where Xxx is either a symbol or a decimal number
public class AddressingCommand(string asmCommand) : IAsmCommand
{
    public string Translate()
    {
        string symbol = asmCommand[1..];
        int address = ushort.Parse(symbol);
        return Convert.ToString(address, 2).PadLeft(16, '0');
    }
}

public class ComputeCommand(string asmCommand) : IAsmCommand
{
    public string Translate()
    {
        // TODO Implement
        return asmCommand;
    }
}

public class LabelCommand(string asmCommand) : IAsmCommand
{
    public string Translate()
    {
        // TODO Implement when symbols are implemented
        return asmCommand;
    }
}

