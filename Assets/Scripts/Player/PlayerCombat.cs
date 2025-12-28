using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerStats _stats;

    public void Use(EnemyController target = null)
    {
        _inventory.GetEquippedItem()?.Use(_stats, target);
    }
}
