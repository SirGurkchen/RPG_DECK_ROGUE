using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerStats _stats;

    public void UseCurrentlySelectedItem(EnemyController target = null)
    {
        if (_inventory != null)
        {
            _inventory.GetEquippedItem()?.Use(_stats, target);
        }
    }

    public void GiveItemToPlayer(ItemBase new_item)
    {
        if (_inventory != null)
        {
            _inventory.GiveItemToInventory(new_item);
        }
        else
        {
            print("Item could not be added to the Inventory!");
        }
    }

    public void EquipItem(int inventory_index)
    {
        if (_inventory.GetItemAtInvetory(inventory_index) != null)
        {
            _inventory.SetEquippedItem(_inventory.GetItemAtInvetory(inventory_index));
        }
    }
}
