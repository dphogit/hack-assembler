namespace Assembler.Services;

/// <summary>
/// Contains all logic for translating assembly mnemonic strings to binary strings.
/// </summary>
public class MnemonicTranslator
{
    private static readonly Dictionary<string, string> jumpTable = new() {
        { "null", "000" },
        { "JGT",  "001" },
        { "JEQ",  "010" },
        { "JGE",  "011" },
        { "JLT",  "100" },
        { "JNE",  "101" },
        { "JLE",  "110" },
        { "JMP",  "111" }
    };

    private static readonly Dictionary<string, string> destTable = new() {
        { "null", "000" },
        { "M",    "001" },
        { "D",    "010" },
        { "MD",   "011" },
        { "A",    "100" },
        { "AM",   "101" },
        { "AD",   "110" },
        { "AMD",  "111" }
    };

    private static readonly Dictionary<string, string> compTable = new() {
        { "0",   "0101010" },
        { "1",   "0111111" },
        { "-1",  "0111010" },
        { "D",   "0001100" },
        { "A",   "0110000" },
        { "!D",  "0001101" },
        { "!A",  "0110001" },
        { "-D",  "0001111" },
        { "-A",  "0110011" },
        { "D+1", "0011111" },
        { "A+1", "0110111" },
        { "D-1", "0001110" },
        { "A-1", "0110010" },
        { "D+A", "0000010" },
        { "D-A", "0010011" },
        { "A-D", "0000111" },
        { "D&A", "0000000" },
        { "D|A", "0010101" },
        { "M",   "1110000" },
        { "!M",  "1110001" },
        { "-M",  "1110011" },
        { "M+1", "1110111" },
        { "M-1", "1110010" },
        { "D+M", "1000010" },
        { "D-M", "1010011" },
        { "M-D", "1000111" },
        { "D&M", "1000000" },
        { "D|M", "1010101" }
    };

    /// <summary>Converts a jump mnemonic to its 3 bit binary representation.</summary>
    /// <returns>A 3 character string (i.e. 3 bits) representing the binary value of the jump mnemonic.</returns>
    public static string JumpToBinary(string jumpMnemonic)
    {
        return jumpTable[jumpMnemonic];
    }

    /// <summary>Converts a dest mnemonic to its 3 bit binary representation.</summary>
    /// <returns>A 3 character string (i.e. 3 bits) representing the binary value of the dest mnemonic.</returns>
    public static string DestToBinary(string destMnemonic)
    {
        return destTable[destMnemonic];
    }

    /// <summary>Converts a comp mnemonic to its 7 bit binary representation.</summary>
    /// <returns>A 7 character string (i.e. 7 bits) representing the binary value of the comp mnemonic.</returns>
    public static string CompToBinary(string compMnemonic)
    {
        return compTable[compMnemonic];
    }
}
