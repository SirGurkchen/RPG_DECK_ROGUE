using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]private List<ItemController> _inventory = new List<ItemController>();
    [SerializeField] private ItemController _equippedItem;

    private const int MAX_INVENTORY_SIZE = 4;
    
    public void SetEquippedItem(int item_index)
    {
        _equippedItem = _inventory[item_index];
    }

    public void GiveItemToInventory(ItemController item)
    {
        if (_inventory.Count < MAX_INVENTORY_SIZE)
        {
            _inventory.Add(item);
        }
    }

    public void DeselectItem()
    {
        _equippedItem = null;
    }

    public ItemController GetEquippedItem()
    {
        return _equippedItem;
    }
}
