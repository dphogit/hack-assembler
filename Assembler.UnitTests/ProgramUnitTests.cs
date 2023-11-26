namespace Assembler.UnitTests;

public class AssemblerUnitTests
{
    [Fact]
    public void Assemble_NoSymbols_TranslatesToBinary()
    {
        // Arrange
        string addProgram = """
        // This file is part of www.nand2tetris.org 
        // and the book \"The Elements of Computing Systems\" by Nisan and
        // Schocken, MIT Press. 
        // File name: projects/06/add/Add.asm

        // Computes R0 = 2 + 3 (R0 refers to RAM[0]).

        @2
        D=A
        @3
        D=D+A
        @0
        M=D
        
        """;

        string expectedBinary = """
        0000000000000010
        1110110000010000
        0000000000000011
        1110000010010000
        0000000000000000
        1110001100001000

        """;

        using var reader = new StringReader(addProgram);
        using var writer = new StringWriter();

        // Act
        Program.Assemble(reader, writer);
        string actualBinary = writer.ToString();

        // Assert
        Assert.Equal(expectedBinary, actualBinary);
    }
}