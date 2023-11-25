namespace Assembler;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            PrintUsage();
            return 1;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File does not exist: {filePath}");
            return 1;
        }

        // Check file extension to be .asm
        if (Path.GetExtension(filePath) != ".asm")
        {
            Console.WriteLine($"File is not an .asm file: {filePath}");
            return 1;
        }

        using StreamReader streamReader = new(filePath);

        Parser parser = new(streamReader);

        var asmCommand = parser.Advance();
        while (asmCommand is not null)
        {
            Console.WriteLine(asmCommand.Translate());
            asmCommand = parser.Advance();
        }

        return 0;
    }

    static void PrintUsage()
    {
        Console.WriteLine("\nUsage: dotnet run <INPUT>");
        Console.WriteLine("\nINPUT:");
        Console.WriteLine("    Path to an .asm file to assemble.\n");
    }
}
