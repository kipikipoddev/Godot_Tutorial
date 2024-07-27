namespace Hex_Space_Rpg.Commands;

public record Remove_Effect_Command(IEffect_Model Effect) : Command(Effect)
{
}