using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthUI;
    [SerializeField] private CardUIManager _cardUI;
    [SerializeField] private TextMeshProUGUI[] _rewardUI;
    [SerializeField] private ItemUI _itemUI;

    private string _defaultHealthText;

    private void Start()
    {
        _defaultHealthText = _healthUI.text;
    }

    public void UpdateWeaponUI(List<ItemController> inventory)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null && inventory[i].gameObject != null)
            {
                _itemUI.SetItemUI(inventory[i].GetItemName(), i);
            }
            else
            {
                _itemUI.SetItemUI("", i);
            }
        }
        _itemUI.DemarkAll();
    }

    public void ClearWeaponUI()
    {
        //_weaponUI.text = "";
    }

    public void MarkSelecteItem(int index)
    {
        _itemUI.MarkItem(index);
    }

    public void UpdateHealthText(int health, int maxHealth)
    {
        _healthUI.text = _defaultHealthText + health + " | " + maxHealth;
    }

    public void AddCardUI(CardController card)
    {
        _cardUI.FillUIWithCard(card);
    }

    public void RemoveCardUI(CardController card)
    {
        _cardUI.RemoveCardFromUI(card);
    }

    public void FillRewardUI(ItemController itemOne, ItemController itemTwo)
    {
        _rewardUI[0].text = itemOne.GetItemName();
        _rewardUI[1].text = itemTwo.GetItemName();
    }

    public void ClearRewardUI()
    {
        _rewardUI[0].text = "";
        _rewardUI[1].text = "";
    }
}
