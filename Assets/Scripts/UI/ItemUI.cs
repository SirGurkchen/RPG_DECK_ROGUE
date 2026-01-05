using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _itemList;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _itemDamage;
    [SerializeField] private TextMeshProUGUI _itemEndurance;

    private const string FIST_TEXT = "Your last resort. Does 2 Damage and ignores any blocking.";

    public void SetItemUI(string itemName, int index)
    {
        if (index >= 0 && index < _itemList.Length)
        {
            _itemList[index].text = itemName;
        }
    }

    public void MarkItem(int index)
    {
        DemarkAll();
        _itemList[index].color = Color.red;
    }

    public void DemarkAll()
    {
        foreach (TextMeshProUGUI item in _itemList)
        {
            if (item != null && item.text != "")
            {
                item.color = Color.white;
            }
        }
    }

    public void ShowItemDescription(ItemController item)
    {
        if (item == null)
        {
            _description.text = FIST_TEXT;
            return;
        }

        _description.text = item.GetItemBase().Description;
        
        if (item.GetItemBase() is Weapon weapon)
        {
            _itemDamage.text = "Damage: " + weapon.Damage;
            _itemEndurance.text = "Endurance: " + item.GetCurrentEndurance();
        }
    }

    public void ShowRewardItemDescription(ItemController item)
    {
        _description.text = item.GetItemBase().Description;

        if (item.GetItemBase() is Weapon weapon)
        {
            _itemDamage.text = "Damage: " + weapon.Damage;
            _itemEndurance.text = "Endurance: " + weapon.MaxDurability;
        }
    }

    public void RemoveDescriptionText()
    {
        _description.text = string.Empty;
        _itemDamage.text = string.Empty;
        _itemEndurance.text= string.Empty;
    }
}
