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
        if (item == null)
        {
            return;
        }

        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i] == null)
            {
                _inventory[i] = item;
                item.OnItemDestroy += ItemDestroyed;
                return;
            }
        }
    }

    public bool CanAddItem()
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if ( _inventory[i] == null || _inventory[i].gameObject == null)
            {
                return true;
            } 
        }
        return false;
    }

    private void ItemDestroyed(ItemController item)
    {
        int itemIndex = _inventory.IndexOf(item);

        if (itemIndex >= 0)
        {
            item.OnItemDestroy -= ItemDestroyed;
            _inventory[itemIndex] = null;
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

    public List<ItemController> GetInventory()
    {
        return _inventory;
    }
}
