using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Orchestrates the overall UI of the game.
/// Controls CardUI, enemy UIs, RewardUI, ItemUI and playey UI elements.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthBarText;
    [SerializeField] private CardUIManager _cardUI;
    [SerializeField] private ItemUI _itemUI;
    [SerializeField] private RewardUI _rewardUI;
    [SerializeField] private TextMeshProUGUI _enemyInfo;
    [SerializeField] private Image _manaBar;
    [SerializeField] private TextMeshProUGUI _manaBarText;
    [SerializeField] private TextMeshProUGUI _coinsUI;
    [SerializeField] private GameObject _selectionPrompts;
    [SerializeField] private GameObject _leftOnPrompt;
    [SerializeField] private GameObject _rightOnPrompt;
    [SerializeField] private GameObject _damageTakenVisual;

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

    public void UpdateHealthBar(int health, int maxHealth)
    {
        float fill = (float)health / maxHealth;

        _healthBar.fillAmount = Mathf.Clamp01(fill);
        _healthBarText.text = health + " | " + maxHealth;
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
        float fill = (float)mana / maxMana;

        _manaBar.fillAmount = Mathf.Clamp01(fill);
        _manaBarText.text = mana + " | " + maxMana;
    }

    public void UpdateCoinsUI(int coins)
    {
        _coinsUI.text =  "" + coins;
    }

    public void ToggleSelectionPrompts(bool isOn)
    {
        _selectionPrompts.gameObject.SetActive(isOn);
    }

    public void ToggleDamageVisual(bool isOn)
    {
        _damageTakenVisual.SetActive(isOn);
    }

    public void ToggleInputPrompt(Input type)
    {
        if (type == Input.Q)
        {
            _rightOnPrompt.SetActive(false);
            _leftOnPrompt.SetActive(!_leftOnPrompt.activeSelf);
        }
        else
        {
            _leftOnPrompt.SetActive(false);
            _rightOnPrompt.SetActive(!_rightOnPrompt.activeSelf);
        }
    }
}

public enum Input
{
    Q,
    E
}
