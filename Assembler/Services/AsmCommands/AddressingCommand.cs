using Assembler.Models;

namespace Assembler.Services.AsmCommands;

public class AddressingCommand(string asmCommand) : IAsmCommand
{
    public bool IsTranslatable => true;

    public string Translate(SymbolTable symbolTable)
    {
        string symbol = asmCommand[1..];
        bool isDecimalAddress = ushort.TryParse(symbol, out ushort address);
        if (isDecimalAddress)
        {
            return Convert.ToString(address, 2).PadLeft(16, '0');
        }
        if (symbolTable.Contains(symbol))
        {
            return Convert.ToString(symbolTable.GetAddress(symbol), 2).PadLeft(16, '0');
        }
        else
        {
            symbolTable.AddVariable(symbol);
            return Convert.ToString(symbolTable.GetAddress(symbol), 2).PadLeft(16, '0');
        }
    }

    public string GetSymbol()
    {
        return asmCommand[1..];
    }
}
