using Assembler.AsmCommands;

namespace Assembler.Parsing;

/// <summary>
/// Reads assembly language commands and parses them, creating commands (<see cref="IAsmCommand"/>) each time the
/// parser advances to a new assembly command which these can be translated into binary instructions in client code.
/// </summary>
class Parser(TextReader reader)
{
    private readonly TextReader reader = reader;
    public bool HasMoreInstructions { get; private set; } = true;

    /// <summary>
    /// Advances the parser to the next command, skipping comments, blank lines and cleaning up any whitespace.
    /// </summary>
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