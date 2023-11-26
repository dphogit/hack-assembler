using Assembler.AsmCommands;

namespace Assembler.Parsing;

public class Parser(TextReader reader)
{
    private readonly TextReader reader = reader;
    public bool HasMoreCommands { get; private set; } = true;

    public IAsmCommand? Advance()
    {
        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            line = line.Split("//")[0].Trim();      // Grabs non-comment portion of line (if any) and trims whitespace
            if (line == "") continue;
            return AsmCommandFactory.CreateCommand(line);
        }
        HasMoreCommands = false;
        return null;
    }
}