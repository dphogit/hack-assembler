using Assembler.Models;

namespace Assembler.Services.AsmCommands;

// L-Command: (Xxx) where Xxx is a symbol
public class LabelCommand(string asmCommand) : IAsmCommand
{
    public bool IsTranslatable => false;

    public string Translate(SymbolTable? symbolTable = null)
    {
        throw new NotImplementedException();
    }

    public string GetSymbol()
    {
        return asmCommand[1..^1];
    }
}
