using System.Text;
using Assembler.Services;

namespace Assembler.IntegrationTests;

public class AssemblerIntegrationTests
{
    private class ReaderWriter
    {
        private readonly MemoryStream readerMemoryStream;
        private readonly MemoryStream writerMemoryStream;
        public StreamReader Reader { get; }
        public StreamWriter Writer { get; }

        public ReaderWriter(string asmProgram)
        {
            readerMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(asmProgram));
            writerMemoryStream = new MemoryStream();
            Reader = new StreamReader(readerMemoryStream);
            Writer = new StreamWriter(writerMemoryStream);
        }

        public string GetWriterString()
        {
            Writer.Flush();
            return Encoding.UTF8.GetString(writerMemoryStream.ToArray());
        }
    }

    private static void AssertAssemblyTranslation(string asmProgram, string expectedBinary)
    {
        var readerWriter = new ReaderWriter(asmProgram);
        AssemblyProcessor assembler = new(readerWriter.Reader, readerWriter.Writer);

        assembler.Assemble();

        string actualBinary = readerWriter.GetWriterString().Replace("\r\n", "\n");
        expectedBinary = expectedBinary.Replace("\r\n", "\n");

        Assert.Equal(expectedBinary, actualBinary);
    }

    [Fact]
    public void Assemble_NoSymbols_TranslatesToBinary()
    {
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

        AssertAssemblyTranslation(addAsmProgram, expectedBinary);
    }

    [Fact]
    public void Assemble_LabelSymbolsOnly_TranslatesToBinary()
    {
        string maxAsmProgram = """
        // This file is part of www.nand2tetris.org
        // and the book "The Elements of Computing Systems"
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

        string expectedBinary = """
        0000000000000000
        1111110000010000
        0000000000000001
        1111010011010000
        0000000000001100
        1110001100000001
        0000000000000001
        1111110000010000
        0000000000000010
        1110001100001000
        0000000000010000
        1110101010000111
        0000000000000000
        1111110000010000
        0000000000000010
        1110001100001000
        0000000000010000
        1110101010000111

        """;

        AssertAssemblyTranslation(maxAsmProgram, expectedBinary);
    }

    [Fact]
    public void Assemble_AllSymbolTypes_TranslatesToBinary()
    {
        string rectAsmProgram = """
        // This file is part of www.nand2tetris.org
        // and the book "The Elements of Computing Systems"
        // by Nisan and Schocken, MIT Press.
        // File name: projects/06/rect/Rect.asm

        // Draws a rectangle at the top-left corner of the screen.
        // The rectangle is 16 pixels wide and R0 pixels high.

            // If (R0 <= 0) goto END else n = R0
            @R0
            D=M
            @END
            D;JLE
            @n
            M=D
            // addr = base address of first screen row
            @SCREEN
            D=A
            @addr
            M=D
        (LOOP)
            // RAM[addr] = -1
            @addr
            A=M
            M=-1
            // addr = base address of next screen row
            @addr
            D=M
            @32
            D=D+A
            @addr
            M=D
            // decrements n and loops
            @n
            M=M-1
            D=M
            @LOOP
            D;JGT
        (END)
            @END
            0;JMP

        """;

        string expectedBinary = """
        0000000000000000
        1111110000010000
        0000000000011000
        1110001100000110
        0000000000010000
        1110001100001000
        0100000000000000
        1110110000010000
        0000000000010001
        1110001100001000
        0000000000010001
        1111110000100000
        1110111010001000
        0000000000010001
        1111110000010000
        0000000000100000
        1110000010010000
        0000000000010001
        1110001100001000
        0000000000010000
        1111110010001000
        1111110000010000
        0000000000001010
        1110001100000001
        0000000000011000
        1110101010000111

        """;

        AssertAssemblyTranslation(rectAsmProgram, expectedBinary);
    }
}
