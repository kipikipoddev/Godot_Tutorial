using Hex_Space_Rpg.Models;

namespace Hex_Space_Rpg.Interfaces;

public interface IEffect_Model : IName_Model
{
    Type_Model Type { get; }
    ITimer_Model Timer { get; }
    bool Is_Friendly { get; }
}