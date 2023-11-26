namespace Assembler.AsmCommands;

public class LabelCommand(string asmCommand) : IAsmCommand
{
    public bool IsTranslatable => false;

    public string Translate()
    {
        throw new NotImplementedException();
    }

    public string GetSymbol()
    {
        return asmCommand[1..^1];   // In the form of (symbol) - remove the parentheses.
    }
}