using TMPro;
using UnityEngine;

/// <summary>
/// Handles the visualization of the Inventory.
/// Shows item descriptions for selected items.
/// </summary>
public class ItemUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemPositions;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _itemDamage;
    [SerializeField] private TextMeshProUGUI _itemEndurance;

    /// <summary>
    /// Sets an item UI.
    /// </summary>
    /// <param name="item">Item to display.</param>
    /// <param name="index">Index of item slot.</param>
    public void SetItemUI(ItemController item, int index)
    {
        if (index >= 0 && index < _itemPositions.Length)
        {
            if (item.GetItemIcon() != null)
            {
                item.GetItemIcon().transform.position = _itemPositions[index].transform.position;
            }
        }
    }

    /// <summary>
    /// Shows an item description.
    /// </summary>
    /// <param name="item">Item to show description for.</param>
    public void ShowItemDescription(ItemController item)
    {
        _description.text = item.GetItemBase().Description;
        
        if (item.GetItemBase() is Weapon weapon)
        {
            _itemDamage.text = "Damage: " + weapon.Damage;

            if (weapon.MaxDurability > 0)
            {
                _itemEndurance.text = "Endurance: " + item.GetCurrentEndurance();
            }
        }
    }

    /// <summary>
    /// Shows the item reward description.
    /// </summary>
    /// <param name="item">Item to show description for.</param>
    public void ShowRewardItemDescription(ItemController item)
    {
        _description.text = item.GetItemBase().Description;

        if (item.GetItemBase() is Weapon weapon)
        {
            _itemDamage.text = "Damage: " + weapon.Damage;
            _itemEndurance.text = "Endurance: " + weapon.MaxDurability;
        }
        else
        {
            _itemDamage.text = string.Empty;
            _itemEndurance.text = string.Empty;
        }
    }

    /// <summary>
    /// Removes all item descriptions.
    /// </summary>
    public void RemoveDescriptionText()
    {
        _description.text = string.Empty;
        _itemDamage.text = string.Empty;
        _itemEndurance.text= string.Empty;
    }
}
