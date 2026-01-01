using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Slash Effect")]
public class SlashEffect : CardEffect
{
    [SerializeField] private int damage = 5;

    public override void Execute(CardContext context)
    {
        EnemyController target = context.player.GetTargetedEnemy();
        if (target != null)
        {
            target.TakeDamage(damage, AttackType.Melee);
        }
    }

    public override string GetDescription()
    {
        return "Dealt " + damage + " damage to the enemy!";
    }
}
