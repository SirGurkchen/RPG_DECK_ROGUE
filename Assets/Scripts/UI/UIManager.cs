using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Orchestrates the overall UI of the game.
/// Controls CardUI, Input UI, enemy UIs, RewardUI, ItemUI and player UI elements.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private CardUIManager _cardUI;
    [SerializeField] private ItemUI _itemUI;
    [SerializeField] private RewardUI _rewardUI;
    [SerializeField] private TextMeshProUGUI _enemyInfo;
    [SerializeField] private GameObject _damageTakenVisual;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseMenuEndPos;
    [SerializeField] private InputUI _inputUI;
    [SerializeField] private StatsUIManager _statsUI;

    private Vector3 _pauseMenuStartPos;

    private const float PAUSE_ANIMATION_TIME = 0.6f;

    private void Start()
    {
        _pauseMenuStartPos = _pauseMenu.transform.position;
    }

    public void UpdateWeaponUI(List<ItemController> inventory)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null && inventory[i].gameObject != null)
            {
                _itemUI.SetItemUI(inventory[i], i);
            }
        }
    }

    public void TogglePauseMenu(bool isOn)
    {
        if (_pauseMenu == null) return;
        _pauseMenu.transform.DOKill();
        AudioManager.Instance.PlayChainSound();
        if (isOn)
        {
            _pauseMenu.transform.DOMove(_pauseMenuEndPos.transform.position, PAUSE_ANIMATION_TIME).SetUpdate(true);
        }
        else
        {
            _pauseMenu.transform.DOMove(_pauseMenuStartPos, PAUSE_ANIMATION_TIME).SetUpdate(true);
        }
    }

    public void ToggleLeavePrompt()
    {
        _inputUI.ToggleLeavePrompt();
    }

    public void ShowItemDescription(ItemController item)
    {
        _itemUI.ShowItemDescription(item);
    }

    public void ShowRewardItemDescription(ItemController item, int index)
    {
        _rewardUI.ShowRewardItemDescription(item, index, _itemUI);
    }

    public void RemoveItemDescription()
    {
        _itemUI.RemoveDescriptionText();
    }

    public void UpdateHealthBar(int health, int maxHealth)
    {
        _statsUI.UpdateHealthBar(health, maxHealth);
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
        _statsUI.UpdateManaUI(mana, maxMana);
    }

    public void UpdateCoinsUI(int coins)
    {
        _statsUI.UpdateCoinsUI(coins);
    }

    public void ToggleSelectionPrompts(bool isOn)
    {
        _inputUI.ToggleSelectionPrompts(isOn);
    }

    public void ToggleDamageVisual(bool isOn)
    {
        _damageTakenVisual.SetActive(isOn);
    }

    public void ToggleInputPrompt(Input type)
    {
        _inputUI.ToggleInputPrompt(type);
    }

    public void ResetInputPrompt()
    {
        _inputUI.ResetInputPrompt();
    }

    public void ToggleCardMenuPrompt(bool isOn)
    {
        _inputUI.ToggleCardMenuPrompt(isOn);
    }
}
