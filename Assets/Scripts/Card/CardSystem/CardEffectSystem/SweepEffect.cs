using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Sweep Effect")]
public class SweepEffect : CardEffect
{
    [SerializeField] private int damage = 3;

    public override void Execute(CardContext context)
    {
        foreach (EnemyController enemy in context.enemies)
        {
            enemy.TakeDamage(damage, AttackType.Melee);
        }
    }

    public override string GetDescription()
    {
        return "Dealt " + damage + " damage to each enemy!";
    }
}
