using UnityEngine;

/// <summary>
/// Contains configuration data for cards.
/// Used to create new Cards with different effects.
/// </summary>
[CreateAssetMenu(menuName = "Cards/New Card")]
public class CardBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _cardName;
    [TextArea]
    [SerializeField] private string _cardDescription;
    [SerializeField] private int _cardShopPrice;

    [Header("Conditional Effects")]
    [Tooltip("Effect which triggers if main condition is met. Leave empty if no Main Effect.")]
    [SerializeField] private CardConditionalWrapper _mainEffect;
    [Tooltip("Effect which triggers if main condition is NOT met, but this condition is met. Leave empty if no Secondary Effect")]
    [SerializeField] private CardConditionalWrapper _secondaryEffect;

    public string Name => _cardName;
    public string Description => _cardDescription;
    public int ShopPrice => _cardShopPrice;

    /// <summary>
    /// Uses the card and determines which effect to use.
    /// </summary>
    /// <param name="context">Context of the card played.</param>
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
    }
}
