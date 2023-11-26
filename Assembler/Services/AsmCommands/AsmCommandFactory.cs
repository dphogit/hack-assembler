namespace Assembler.Services.AsmCommands;

public class AsmCommandFactory
{
    public static IAsmCommand CreateCommand(string commandStatement)
    {
        return commandStatement[0] switch
        {
            '@' => new AddressingCommand(commandStatement),
            '(' => new LabelCommand(commandStatement),
            _ => new ComputeCommand(commandStatement)
        };
    }
}
