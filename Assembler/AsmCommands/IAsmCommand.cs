namespace Assembler.AsmCommands;

public interface IAsmCommand
{
    /// <summary>Translates the constructor passed assembly command into its 16-bit binary representation.</summary>
    /// <returns>The 16-bit binary string representing the assembly command.</returns>
    public string Translate();
}

