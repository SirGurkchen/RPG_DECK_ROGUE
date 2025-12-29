using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<ItemController> _inventory = new List<ItemController>();
    [SerializeField] private ItemController _equippedItem;

    private const int MAX_INVENTORY_SIZE = 4;
    
    public void SetEquippedItem(ItemController item)
    {
        _equippedItem = item;
    }

    public void GiveItemToInventory(ItemController item)
    {
        if (_inventory.Count < MAX_INVENTORY_SIZE)
        {
            _inventory.Add(item);
        }
    }

    public ItemController GetEquippedItem()
    {
        return _equippedItem;
    }
}
