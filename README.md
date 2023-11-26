# Nand2Tetris Hack Assembler

This repository contains an implementation of an assembler for the Hack computer in project 06 of
the [Nand2Tetris](https://www.nand2tetris.org/) course. Written in C# (.NET 8.0), the assembler
command line program translates input Hack assembly `.asm` code files into `.hack` binary
instruction files. The assembler is written in C# (.NET 8.0).

The program satisfies the requirements of the project, successful when comparing it's outputs to the
provided files by the project. Obviously, it can be made more robust and probably implemented
better, but this is more of a learning exercise rather than software engineering focused.
If I'm feeling cute, I might do a robust implementation in a lower level language like C++ or Rust.

## 💻 Usage

The command line program (`Assembler` project) can be run with the `dotnet run` command. The program takes a single argument, the path to the `.asm` file to be assembled. The output is a `.hack` file
in the same directory as the input file. e.g. `Add.asm -> Add.hack`.

```bash
Usage: dotnet run <FILE_PATH>
Arguments:
    FILE_PATH   Path to the Hack .asm file to be assembled.
```

## 🧪 Testing

Minimal integration tests have been written to cover the cases of:

- Predefined symbols only
- Predefined, and Label symbols
- All symbols (predefined, label, and user variables)

Sample files that users can use to test the assembler are provided in the `Programs` directory.

To run the tests, simply use the `dotnet test` command with the appropriate options as with any
other .NET Core test project.

The Assembler provided by Nand2Tetris can also be used to compare the outputs of your assembler with
the correct translation, which you will need to install for yourself.
