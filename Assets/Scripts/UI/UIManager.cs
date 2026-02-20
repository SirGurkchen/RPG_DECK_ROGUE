using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Orchestrates the overall UI of the game.
/// Controls CardUI, enemy UIs, RewardUI, ItemUI and playey UI elements.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthUI;
    [SerializeField] private CardUIManager _cardUI;
    [SerializeField] private ItemUI _itemUI;
    [SerializeField] private RewardUI _rewardUI;
    [SerializeField] private TextMeshProUGUI _enemyInfo;
    [SerializeField] private TextMeshProUGUI _manaUI;

    private string _defaultHealthText;
    private string _defaultManaText;

    private void Start()
    {
        _defaultHealthText = _healthUI.text;
        _defaultManaText = _manaUI.text;
    }

    public void UpdateWeaponUI(List<ItemController> inventory)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null && inventory[i].gameObject != null)
            {
                _itemUI.SetItemUI(inventory[i], i);
            }
            else
            {
                _itemUI.SetEmptyItem(i);
            }
        }
        _itemUI.DemarkAll();
    }

    public void MarkSelectItem(int index)
    {
        _itemUI.MarkItem(index);
    }

    public void ShowItemDescription(ItemController item)
    {
        _itemUI.ShowItemDescription(item);
    }

    public void ShowRewardItemDescription(ItemController item, int index)
    {
        _rewardUI.ShowRewardItemDescription(item, index, _itemUI);
    }

    public void DemarkAllRewards()
    {
        _rewardUI.DemarkAllRewards();
    }

    public void RemoveItemDescription()
    {
        _itemUI.RemoveDescriptionText();
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
        _rewardUI.FillRewardUI(itemOne, itemTwo);
    }

    public void ClearRewardUI()
    {
        _rewardUI.ClearRewardUI();
    }

    public void ShowFistDescription()
    {
        _itemUI.ShowItemDescription(null);
    }

    public void DemarkItems()
    {
        _itemUI.DemarkAll();
    }

    public void ShowEnemyInfo(EnemyController enemy)
    {
        if (enemy != null)
        {
            _enemyInfo.text = enemy.GetEnemyName();
        }
        else
        {
            _enemyInfo.text = string.Empty;
        }
    }

    public void UpdateManaUI(int mana, int maxMana)
    {
        _manaUI.text = _defaultManaText + mana + " | " + maxMana;
    }
}
