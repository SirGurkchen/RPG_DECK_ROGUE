using UnityEngine;

/// <summary>
/// Conditional Class for triggering Card Effects.
/// Used for defining conditionals for card usage.
/// Designers set needed Weapon or Attack Type for main or secondary Attack.
/// </summary>
[System.Serializable]
public class CardConditionalWrapper
{
    [Tooltip("Leave empty if general or secondary effect use")]
    public ItemBase requiredItem;

    [Tooltip("Set as None for general attack type or main effect use")]
    public AttackType requiredAttackType;

    public CardEffect effect;

    public bool ItemConditionMet(CardContext context)
    {
        bool itemMatch = requiredItem == null || context.usedItem == requiredItem;
        return itemMatch;
    }

    public bool AttackTypeConditionMet(CardContext context)
    {
        bool attackMatch = requiredAttackType == AttackType.None || context.attackType == requiredAttackType;
        return attackMatch;
    }
}
