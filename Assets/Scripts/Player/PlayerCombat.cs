using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;

    private const int FIST_DAMAGE = 2;

    public bool Use(PlayerStats stats, EnemyController target = null)
    {
        if (_inventory.GetEquippedItem() != null)
        {
            ItemController item = _inventory.GetEquippedItem();
            return item.Use(stats, target);
        }
        else
        {
            target.TakeDamage(FIST_DAMAGE, AttackType.None);
            return true;
        }
    }

    public void TakeDamage(PlayerStats stats, int damage)
    {
        stats.ReceiveDamage(damage);
    }
}
