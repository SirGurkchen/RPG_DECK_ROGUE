using UnityEngine;

public class CardContext
{
    public ItemBase usedItem;
    public AttackType attackType;
    public EnemyController[] enemies;
    public PlayerManager player;
}

public abstract class CardEffect : ScriptableObject
{
    public abstract void Execute(CardContext context);
    public abstract string GetDescription();
}
