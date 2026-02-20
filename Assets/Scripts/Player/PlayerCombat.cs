using UnityEngine;

/// <summary>
/// Handles the logic of player combat, so Attacking and taking damage.
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;

    public bool Use(PlayerStats stats, EnemyController target = null)
    {
        if (_inventory.GetEquippedItem() != null)
        {
            ItemController item = _inventory.GetEquippedItem();
            return item.Use(stats, target);
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(PlayerStats stats, int damage)
    {
        stats.ReceiveDamage(damage);
    }
}
