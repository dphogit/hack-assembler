using Assembler.AsmCommands;

namespace Assembler;

/// <summary>
/// Reads assembly language instructions and parses them, providing convenient access to their underlying
/// components (i.e. fields and symbols) that make it up. e.g. @Xxx, D=A, M=D+1, (LOOP), etc.
/// </summary>
class Parser(TextReader reader)
{
    private readonly TextReader reader = reader;
    public bool HasMoreInstructions { get; private set; } = true;

    /// <summary>Advances the parser to the next command, skipping comments and blank lines.</summary>
    public IAsmCommand? Advance()
    {
        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            line = line.Split("//")[0].Trim();      // Grabs non-comment portion of line (if any) and trims whitespace
            if (line == "") continue;
            return AsmCommandFactory.CreateCommand(line);
        }
        HasMoreInstructions = false;
        return null;
    }
}