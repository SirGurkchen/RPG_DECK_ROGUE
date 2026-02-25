using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player item inventory.
/// Also incorporates checking for first item equip.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemController> _inventory;
    [SerializeField] private ItemController _equippedItem;

    private const int MAX_INVENTORY_SIZE = 5;

    public event Action<CardController, ItemController> OnCardWasUnlocked;

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
        if (item_index >= _inventory.Count)
        {
            _equippedItem = null;
            return;
        }

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
                item.OnItemFirstAddedToInventory += FirstEquip;
                if (!item.CheckCardUnlock())
                {
                    item.OnItemFirstAddedToInventory -= FirstEquip;
                }
                return;
            }
        }
    }

    private void FirstEquip(ItemController item)
    {
        OnCardWasUnlocked?.Invoke(item.GetItemBase().UnlockCard, item);
        item.OnItemFirstAddedToInventory -= FirstEquip;
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
