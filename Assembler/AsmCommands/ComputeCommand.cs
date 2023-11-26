namespace Assembler.AsmCommands;

public class ComputeCommand(string asmCommand) : IAsmCommand
{
    public bool IsTranslatable => true;

    public string Translate(SymbolTable symbolTable)
    {
        string comp;
        string dest = "null";
        string jump = "null";

        // The command is in the form of dest=comp or comp;jump.
        if (asmCommand.Contains('='))
        {
            string[] parts = asmCommand.Split('=');
            (dest, comp) = (parts[0], parts[1]);
        }
        else
        {
            string[] parts = asmCommand.Split(';');
            (comp, jump) = (parts[0], parts[1]);
        }

        string jumpBinary = MnemonicTranslator.JumpToBinary(jump);
        string compBinary = MnemonicTranslator.CompToBinary(comp);
        string destBinary = MnemonicTranslator.DestToBinary(dest);

        return $"111{compBinary}{destBinary}{jumpBinary}";
    }
}