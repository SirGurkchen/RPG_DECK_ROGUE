using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;

    public void Use(PlayerStats stats, EnemyController target = null)
    {
        if (_inventory.GetEquippedItem() != null)
        {
            _inventory.GetEquippedItem().Use(stats, target);
        }
        else
        {
            Debug.Log("No Weapon Equipped!");
        }
    }

    public void TakeDamage(PlayerStats stats, int damage)
    {
        stats.ReceiveDamage(damage);
    }
}
