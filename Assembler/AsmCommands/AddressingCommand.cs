namespace Assembler.AsmCommands;

public class AddressingCommand(string asmCommand) : IAsmCommand
{
    public bool IsTranslatable => true;

    public string Translate()
    {
        // A-Command: @Xxx where Xxx is either a symbol or a decimal number
        string symbol = asmCommand[1..];
        int address = ushort.Parse(symbol);
        return Convert.ToString(address, 2).PadLeft(16, '0');
    }
}