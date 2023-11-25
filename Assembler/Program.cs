using Assembler.FileHandling;

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

        string inputFilePath = args[0];

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"File does not exist: {inputFilePath}");
            return 1;
        }

        if (Path.GetExtension(inputFilePath) != ".asm")
        {
            Console.WriteLine($"File is not an .asm file: {inputFilePath}");
            return 1;
        }

        string outputFilePath = Path.ChangeExtension(inputFilePath, ".hack");

        Assemble(inputFilePath, outputFilePath);

        return 0;
    }

    static void PrintUsage()
    {
        Console.WriteLine("\nUsage: dotnet run <INPUT>");
        Console.WriteLine("\nINPUT:");
        Console.WriteLine("    Path to an .asm file to assemble.\n");
    }

    static void Assemble(string inputFilePath, string outputFilePath)
    {
        using StreamReader streamReader = new(inputFilePath);
        using StreamWriter streamWriter = new(outputFilePath);

        Parser parser = new(streamReader);

        var asmCommand = parser.Advance();
        while (asmCommand is not null)
        {
            string binaryInstruction = asmCommand.Translate();
            streamWriter.WriteLine(binaryInstruction);
            asmCommand = parser.Advance();
        }

        streamReader.Close();
        streamWriter.Close();
    }
}
