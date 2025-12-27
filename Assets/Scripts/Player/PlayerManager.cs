using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerStats _stats;

    public void UseCurrentlySelectedItem(EnemyController target = null)
    {
        if (_inventory != null)
        {
            _inventory.GetCurrentItem()?.Use(_stats, target);
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
