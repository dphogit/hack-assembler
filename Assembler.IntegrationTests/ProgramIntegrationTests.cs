namespace Assembler.IntegrationTests;

public class ProgramIntegrationTests
{
    [Fact]
    public void Assemble_NoSymbols_TranslatesToBinary()
    {
        // Arrange
        string addAsmProgram = """
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

        using var reader = new StringReader(addAsmProgram);
        using var writer = new StringWriter();

        // Act
        Program.Assemble(reader, writer);
        string actualBinary = writer.ToString();

        // Assert
        Assert.Equal(expectedBinary, actualBinary);
    }

    [Fact]
    public void FirstPass_MaxAsm_AddsToSymbolTable()
    {
        // Arrange
        string maxAsmProgram = """
        // This file is part of www.nand2tetris.org
        // and the book \"The Elements of Computing Systems\"
        // by Nisan and Schocken, MIT Press.
        // File name: projects/06/max/Max.asm

        // Computes R2 = max(R0, R1)  (R0,R1,R2 refer to RAM[0],RAM[1],RAM[2])

            // D = R0 - R1
            @R0
            D=M
            @R1
            D=D-M
            // If (D > 0) goto ITSR0
            @ITSR0
            D;JGT
            // Its R1
            @R1
            D=M
            @R2
            M=D
            @END
            0;JMP
        (ITSR0)
            @R0             
            D=M
            @R2
            M=D
        (END)
            @END
            0;JMP
        """;

        using var reader = new StringReader(maxAsmProgram);

        // Act
        var symbolTable = Program.FirstPass(reader);

        // Assert
        Assert.Equal(12, symbolTable.GetAddress("ITSR0"));
        Assert.Equal(16, symbolTable.GetAddress("END"));
    }
}