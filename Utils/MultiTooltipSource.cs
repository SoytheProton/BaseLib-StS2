using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace BaseLib.Utils;

public class MultiTooltipSource
{
    private readonly Func<CardModel, IEnumerable<IHoverTip>> _makeTip;
    
    public MultiTooltipSource(Func<CardModel, IEnumerable<IHoverTip>> tip)
    {
        _makeTip = tip;
    }

    public IEnumerable<IHoverTip> Tips(CardModel card) => _makeTip(card);
}
