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

    // Ensures consistent line endings
    private static void AssertBinaryContents(string expected, string actual)
    {
        expected = expected.Replace("\r\n", "\n");
        actual = actual.Replace("\r\n", "\n");
        Assert.Equal(expected, actual);
    }

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
        var readerWriter = new ReaderWriter(addAsmProgram);
        AssemblyProcessor assembler = new(readerWriter.Reader, readerWriter.Writer);

        // Act
        assembler.Assemble();

        // Assert
        string actualBinary = readerWriter.GetWriterString();
        AssertBinaryContents(expectedBinary, actualBinary);
    }
}
