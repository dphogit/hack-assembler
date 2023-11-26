using Assembler.Models;
using Assembler.Services.AsmCommands;

namespace Assembler.Services;

public class AssemblyProcessor(StreamReader reader, StreamWriter writer)
{
    private readonly Parser parser = new(reader);
    private readonly StreamWriter writer = writer;

    public void Assemble()
    {
        SymbolTable symbolTable = FirstPass();
        parser.Reset();
        SecondPass(symbolTable);
    }

    // Construct the symbol table, add all the L pseudo-commands and their addresses to it and return it.
    private SymbolTable FirstPass()
    {
        SymbolTable symbolTable = new();

        int instructionAddress = 0;
        var asmCommand = parser.Advance();
        while (asmCommand is not null)
        {
            if (asmCommand is LabelCommand labelCommand)
            {
                string symbol = labelCommand.GetSymbol();
                symbolTable.AddEntry(symbol, instructionAddress);
            }
            else
            {
                instructionAddress++;
            }
            asmCommand = parser.Advance();
        }

        return symbolTable;
    }

    // Translate all the A (and resolve/replace symbols with addresses) and C commands, writing them to the stream.
    private void SecondPass(SymbolTable symbolTable)
    {
        IAsmCommand? asmCommand = parser.Advance();
        while (asmCommand is not null)
        {
            if (asmCommand.IsTranslatable)
            {
                string binaryInstruction = asmCommand.Translate(symbolTable);
                writer.WriteLine(binaryInstruction);
            }
            asmCommand = parser.Advance();
        }
    }
}
