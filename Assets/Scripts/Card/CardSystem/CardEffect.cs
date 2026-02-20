using UnityEngine;

/// <summary>
/// Necessary Context Data that has to be passed into each card effect execution.
/// </summary>
public class CardContext
{
    public ItemBase usedItem;
    public AttackType attackType;
    public EnemyController[] enemies;
    public PlayerManager player;
}

/// <summary>
/// Contains methods that each new Card Effect has to include to work.
/// </summary>
public abstract class CardEffect : ScriptableObject
{
    public abstract void Execute(CardContext context);
    public abstract string GetDescription();
}
