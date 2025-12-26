using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private ItemBase _current_item;
    
    public void SetCurrentItem(ItemBase item)
    {
        _current_item = item;
        DebugWeaponGive();
    }

    private void DebugWeaponGive()
    {
        print(_current_item.GetItemToString());
    }
}
