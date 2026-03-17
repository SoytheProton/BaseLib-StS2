using HarmonyLib;
using MegaCrit.Sts2.Core.Models;

namespace BaseLib.Abstracts;

/// <summary>
/// Your power can either inherit CustomPowerModel directly, or a different power class and ICustomPowerModel.
/// This class exists mainly to avoid needing to inherit multiple classes for most powers.
/// </summary>
public abstract class CustomPowerModel : PowerModel, ICustomPower
{
    public virtual string? CustomPackedIconPath => null; //64x64
    public virtual string? CustomBigIconPath => null; //256x256
    public virtual string? CustomBigBetaIconPath => null; //256x256
}