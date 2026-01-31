using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _itemList;
    [SerializeField] private GameObject[] _itemPositions;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _itemDamage;
    [SerializeField] private TextMeshProUGUI _itemEndurance;

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

    public void SetEmptyItem(int index)
    {
        if (index >= 0 && index < _itemList.Length)
        {
            _itemList[index].text = "";
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
