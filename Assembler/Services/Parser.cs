using Assembler.Services.AsmCommands;

namespace Assembler.Services;

public class Parser(StreamReader reader)
{
    private readonly StreamReader reader = reader;

    public IAsmCommand? Advance()
    {
        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            line = line.Split("//")[0].Trim();      // Grabs non-comment portion of line (if any) and trims whitespace
            if (line == "") continue;
            return AsmCommandFactory.CreateCommand(line);
        }
        return null;
    }

    public void Reset()
    {
        reader.DiscardBufferedData();
        reader.BaseStream.Seek(0, SeekOrigin.Begin);
    }
}
