namespace Hex_Space_Rpg.Core;

public interface IHandler<TCommand>
    where TCommand : Command
{
    void Handle(TCommand cmd);
}
