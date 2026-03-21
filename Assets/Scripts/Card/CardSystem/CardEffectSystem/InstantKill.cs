using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Instant Kill")]
public class InstantKill : CardEffect
{
    public override void Execute(CardContext context)
    {
        EnemyController target = context.player.GetTargetedEnemy();
        if (target != null)
        {
            target.TakeDamage(10000, AttackType.None);
            context.player.GetPlayerInventory().GetEquippedItem().DestroyItem();
        }
    }

    public override string GetDescription()
    {
        return "Killed targeted enemy!";
    }
}
