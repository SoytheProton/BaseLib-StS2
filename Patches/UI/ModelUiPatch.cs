using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Potions;
using MegaCrit.Sts2.Core.Nodes.Relics;

// ReSharper disable SuspiciousTypeConversion.Global

namespace BaseLib.Patches.UI;

/// <summary>
/// When a model (card, relic, potion?) is attached to its NCard/NRelic etc, it can provide a Control which will be added as a child of the node.
/// This child will automatically be removed when the model is unbound from the node.
/// </summary>


public interface ICustomUiModel
{
    /// <summary>
    /// Set up a control that will be added as a child. This control should not actively track information, only display it.
    /// Keep any information in the model, as the control will be recreated whenever the model is reloaded.
    /// </summary>
    /// <param name="toAdd">The control that will be added as a child. Add what you want to add as a child of this control.</param>
    /// <returns></returns>
    public void CreateCustomUi(Control toAdd);
}

static class ModelUiPatch
{
    [HarmonyPatch(typeof(NCard), "Reload")]
    static class CardUi
    {
        [HarmonyPostfix]
        static void Postfix(NCard __instance)
        {
            Recreate(__instance, __instance.Model);
        }
    }
    
    [HarmonyPatch(typeof(NRelic), "Reload")]
    static class RelicUi
    {
        [HarmonyPostfix]
        static void Postfix(NRelic __instance)
        {
            Recreate(__instance, __instance.Model);
        }
    }
    
    [HarmonyPatch(typeof(NPotion), "Reload")]
    static class PotionUi
    {
        [HarmonyPostfix]
        static void Postfix(NPotion __instance)
        {
            Recreate(__instance, __instance.Model);
        }
    }

    private static void Recreate(Node n, object? model)
    {
        foreach (var child in n.GetChildren())
        {
            if (child is not NTemporaryUi) continue;
            
            child.Name += "_TRASH";
            child.QueueFreeSafely();
        }

        if (model is not ICustomUiModel customUi) return;
        
        var tempNode = new NTemporaryUi();
        tempNode.Name = model.GetType().Name + "_TEMP";
        customUi.CreateCustomUi(tempNode);
        n.AddChild(tempNode);
        tempNode.Owner = n;
    }
}

internal partial class NTemporaryUi : Control;