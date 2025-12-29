using UnityEngine;

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
            Debug.Log("No Weapon Equipped!");
            return false;
        }
    }

    public void TakeDamage(PlayerStats stats, int damage)
    {
        stats.ReceiveDamage(damage);
    }
}
