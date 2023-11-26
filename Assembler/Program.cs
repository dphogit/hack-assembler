using Assembler.Parsing;

namespace Assembler;

public class Program
{
    public static int Main(string[] args)
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

        using TextReader reader = new StreamReader(inputFilePath);
        using TextWriter writer = new StreamWriter(outputFilePath);

        Assemble(reader, writer);

        return 0;
    }

    public static void Assemble(TextReader reader, TextWriter writer)
    {
        Parser parser = new(reader);

        var asmCommand = parser.Advance();
        while (asmCommand is not null)
        {
            string binaryInstruction = asmCommand.Translate();
            writer.WriteLine(binaryInstruction);
            asmCommand = parser.Advance();
        }

        reader.Close();
        writer.Close();
    }

    static void PrintUsage()
    {
        Console.WriteLine("\nUsage: dotnet run <FILE_PATH>");
        Console.WriteLine("\nArguments:");
        Console.WriteLine("  <FILE_PATH> The path to an .asm file to translate from assembly to binary instructions.\n");
    }
}
