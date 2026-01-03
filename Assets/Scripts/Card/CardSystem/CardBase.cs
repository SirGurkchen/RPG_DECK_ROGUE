using UnityEngine;

[CreateAssetMenu(menuName = "Cards/New Card")]
public class CardBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _cardName;
    [TextArea]
    [SerializeField] private string _cardDescription;

    [Header("Conditional Effects")]
    [SerializeField] private CardConditionalWrapper _mainEffect;
    [SerializeField] private CardConditionalWrapper _secondaryEffect;

    public string Name => _cardName;
    public string Description => _cardDescription;

    public void Play(CardContext context)
    {
        if (_mainEffect.ItemConditionMet(context))
        {
            _mainEffect.effect.Execute(context);
        }
        else if (_secondaryEffect.AttackTypeConditionMet(context))
        {
            _secondaryEffect.effect.Execute(context);
        }
        else
        {
            Debug.Log("Card Had No Effect!");
        }
    }

    public string GetCardName()
    {
        return _cardName;
    }

    public string GetCardDescription()
    {
        return _cardDescription;
    }
}
