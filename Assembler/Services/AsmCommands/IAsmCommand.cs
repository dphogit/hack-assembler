using Assembler.Models;

namespace Assembler.Services.AsmCommands;

public interface IAsmCommand
{
    public bool IsTranslatable { get; }

    /// <summary>
    /// Translates the constructor passed assembly command into its 16-bit binary instruction. A translation is only
    /// possible if the type of command is translatable, therefore the <see cref="IAsmCommand.IsTranslatable"/>
    /// property can be used to check if a translation is possible before invoking this method, otherwise a
    /// `NotImplementedException` will be thrown.
    /// </summary>
    /// <param name="symbolTable">The symbol table to use for translating the command.</param>
    /// <returns>The 16-bit binary instruction string representing the assembly command.</returns>
    /// <exception cref="NotImplementedException">Thrown if the command is not translatable.</exception>
    public string Translate(SymbolTable symbolTable);
}
