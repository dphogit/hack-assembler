namespace Assembler;

/// <summary>
/// The symbol table holds the mapping between symbolic labels in the assembly file to numeric addresses, required
/// for the translation of assembly language into binary instructions.
/// </summary>
public class SymbolTable
{
    public readonly int MaxAddress = 32767;
    private int nextAvailableAddress = 16;

    private readonly Dictionary<string, int> table = new() {
        { "SP", 0 },
        { "LCL", 1 },
        { "ARG", 2 },
        { "THIS", 3 },
        { "THAT", 4 },
        { "R0", 0 },
        { "R1", 1 },
        { "R2", 2 },
        { "R3", 3 },
        { "R4", 4 },
        { "R5", 5 },
        { "R6", 6 },
        { "R7", 7 },
        { "R8", 8 },
        { "R9", 9 },
        { "R10", 10 },
        { "R11", 11 },
        { "R12", 12 },
        { "R13", 13 },
        { "R14", 14 },
        { "R15", 15 },
        { "SCREEN", 16384 },
        { "KBD", 24576 }
    };

    public void AddVariable(string symbol)
    {
        if (table.ContainsKey(symbol))
            return;

        if (nextAvailableAddress > MaxAddress)
            throw new InvalidOperationException($"Unable to add variable {symbol}: memory is full.");

        table.Add(symbol, nextAvailableAddress);
        nextAvailableAddress++;
    }

    public void AddEntry(string symbol, int address)
    {
        if (address < 0 || address > MaxAddress)
            throw new ArgumentOutOfRangeException(nameof(address), $"Address must be between 0 and {MaxAddress}.");

        table.Add(symbol, address);
    }

    public bool Contains(string symbol)
    {
        return table.ContainsKey(symbol);
    }

    public int GetAddress(string symbol)
    {
        return table[symbol];
    }
}