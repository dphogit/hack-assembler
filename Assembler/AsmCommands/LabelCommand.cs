namespace Assembler.AsmCommands;

public class LabelCommand(string asmCommand) : IAsmCommand
{
    public string Translate()
    {
        // TODO Implement when symbols are implemented
        return asmCommand;
    }
}