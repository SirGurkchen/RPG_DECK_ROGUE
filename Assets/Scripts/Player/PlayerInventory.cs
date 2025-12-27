using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private ItemBase _current_item;
    
    public void SetCurrentItem(ItemBase item)
    {
        _current_item = item;
        DebugWeaponGive();
    }

    public void UseCurrentItem(PlayerStats player)
    {
        _current_item.Use(player);
    }

    public ItemType GetCurrentItemType()
    {
        return _current_item.GetItemType();
    }

    public ItemBase GetCurrentItem()
    {
        return _current_item;
    }

    private void DebugWeaponGive()
    {
        print(_current_item.GetItemToString());
    }
}
