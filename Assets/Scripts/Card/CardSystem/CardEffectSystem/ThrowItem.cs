using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Throw Item")]
public class ThrowItem : CardEffect
{
    public override void Execute(CardContext context)
    {
        EnemyController target = context.player.GetTargetedEnemy();
        if (target != null)
        {
            if (context.usedItem is Weapon weapon)
            {
                target.TakeDamage(weapon.Damage * 2, AttackType.None);
            }
            context.player.GetPlayerInventory().GetEquippedItem().DestroyItem();
        }
    }

    public override string GetDescription()
    {
        return "Dealt damage double base attack damage.";
    }
}
