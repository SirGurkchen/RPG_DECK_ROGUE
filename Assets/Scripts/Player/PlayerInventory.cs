using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemController> _inventory;
    [SerializeField] private ItemController _equippedItem;

    private const int MAX_INVENTORY_SIZE = 4;

    private void Awake()
    {
        _inventory = new List<ItemController>(MAX_INVENTORY_SIZE);
        for (int i = 0; i < MAX_INVENTORY_SIZE; i++)
        {
            _inventory.Add(null);
        }
    }

    public void SetEquippedItem(int item_index)
    {
        if (_inventory[item_index] != null)
        {
            _equippedItem = _inventory[item_index];
        }
    }

    public void GiveItemToInventory(ItemController item)
    {
        foreach (ItemController ownedItem in _inventory)
        {
            if (ownedItem == null)
            {
                int itemIndex = _inventory.IndexOf(ownedItem);
                _inventory[itemIndex] = item;
                item.OnItemDestroy += ItemDetroyed;
                break;
            }
        }
    }

    public bool CanAddItem()
    {
        return _inventory.Count < MAX_INVENTORY_SIZE || _inventory.Contains(null);
    }

    private void ItemDetroyed(ItemController item)
    {
        int itemIndex = _inventory.IndexOf(item);

        _inventory[itemIndex] = null;
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
