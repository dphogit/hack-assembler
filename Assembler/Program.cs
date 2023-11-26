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

        using StreamReader reader = new(inputFilePath);
        using StreamWriter writer = new(outputFilePath);

        AssemblyProcessor assembler = new(reader, writer);
        assembler.Assemble();

        return 0;
    }

    static void PrintUsage()
    {
        Console.WriteLine("\nUsage: dotnet run <FILE_PATH>");
        Console.WriteLine("\nArguments:");
        Console.WriteLine("  <FILE_PATH> The path to an .asm file to translate from assembly to binary instructions.\n");
    }
}
