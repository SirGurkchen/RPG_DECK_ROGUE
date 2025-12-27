using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<ItemBase> _inventory = new List<ItemBase>();
    private ItemBase _equippedItem;

    private const int MAX_INVENTORY_SIZE = 4;
    
    public void SetEquippedItem(ItemBase item)
    {
        _equippedItem = item;
    }

    public void UseCurrentItem(PlayerStats player)
    {
        _equippedItem.Use(player);
    }

    public void GiveItemToInventory(ItemBase item)
    {
        if (_inventory.Count < MAX_INVENTORY_SIZE)
        {
            _inventory.Add(item);
        }
    }

    public ItemType GetCurrentItemType()
    {
        return _equippedItem.GetItemType();
    }

    public ItemBase GetEquippedItem()
    {
        return _equippedItem;
    }

    public ItemBase GetItemAtInvetory(int index)
    {
        return _inventory[index];
    }
}
