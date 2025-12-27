using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerStats _stats;

    public void AttackWithItem(EnemyBase target)
    {
        if (_inventory != null)
        {
            ItemBase item = _inventory.GetCurrentItem();
            if (item is Weapon weapon)
            {
                weapon.Use(_stats, target);
            }
        }
    }

    public void GiveItemToPlayer(ItemBase new_item)
    {
        if (_inventory != null)
        {
            _inventory.SetCurrentItem(new_item);
        }
        else
        {
            print("Error!");
        }
    }
}
