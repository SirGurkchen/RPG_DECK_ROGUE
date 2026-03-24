using UnityEngine;

/// <summary>
/// Handles the logic of player combat, so Attacking and taking damage.
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;

    /// <summary>
    /// Uses an item.
    /// </summary>
    /// <param name="stats">Player stats.</param>
    /// <param name="target">Targeted enemy.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Take damage.
    /// </summary>
    /// <param name="stats">Player stats.</param>
    /// <param name="damage">Damage to be taken.</param>
    public void TakeDamage(PlayerStats stats, int damage)
    {
        stats.ReceiveDamage(damage);
    }
}
