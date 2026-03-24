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
    [SerializeField] private TextMeshProUGUI _roundCounter;
    [SerializeField] private GameObject _deathScreen;

    private Vector3 _pauseMenuStartPos;
    private string _defaultRoundText;

    private const float PAUSE_ANIMATION_TIME = 0.6f;

    private void Start()
    {
        _pauseMenuStartPos = _pauseMenu.transform.position;
        _defaultRoundText = "Round: ";
        _roundCounter.text = _defaultRoundText + "1";
    }

    /// <summary>
    /// Updates Inventory UI.
    /// </summary>
    /// <param name="inventory">List of items.</param>
    public void UpdateInventoryUI(List<ItemController> inventory)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null && inventory[i].gameObject != null)
            {
                _itemUI.SetItemUI(inventory[i], i);
            }
        }
    }

    /// <summary>
    /// Updates the round counter.
    /// </summary>
    /// <param name="roundNumber">Current round number.</param>
    public void UpdateRoundCounter(int roundNumber)
    {
        _roundCounter.text = _defaultRoundText + roundNumber;
    }

    /// <summary>
    /// Sets activity of the pause menu.
    /// </summary>
    /// <param name="isOn">Activity of pause menu.</param>
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

    /// <summary>
    /// Toggles the leave prompt.
    /// </summary>
    public void ToggleLeavePrompt()
    {
        _inputUI.ToggleLeavePrompt();
    }

    /// <summary>
    /// Shows Item description.
    /// </summary>
    /// <param name="item">Item to show description for.</param>
    public void ShowItemDescription(ItemController item)
    {
        _itemUI.ShowItemDescription(item);
    }

    /// <summary>
    /// Shows Reward Item description.
    /// </summary>
    /// <param name="item">Item to show description for.s</param>
    /// <param name="index">Index of item.</param>
    public void ShowRewardItemDescription(ItemController item, int index)
    {
        _rewardUI.ShowRewardItemDescription(item, index, _itemUI);
    }

    /// <summary>
    /// Removes item description.
    /// </summary>
    public void RemoveItemDescription()
    {
        _itemUI.RemoveDescriptionText();
    }

    /// <summary>
    /// Updates the health bar.
    /// </summary>
    /// <param name="health">Current health.</param>
    /// <param name="maxHealth">Maximum health.</param>
    public void UpdateHealthBar(int health, int maxHealth)
    {
        _statsUI.UpdateHealthBar(health, maxHealth);
    }

    /// <summary>
    /// Adds a card to the UI.
    /// </summary>
    /// <param name="card">Card to add.</param>
    public void AddCardUI(CardController card)
    {
        _cardUI.FillUIWithCard(card);
    }

    /// <summary>
    /// Removes a card from the UI.
    /// </summary>
    /// <param name="card">Card to remove.</param>
    public void RemoveCardUI(CardController card)
    {
        _cardUI.RemoveCardFromUI(card);
    }

    /// <summary>
    /// Fills reward UI.
    /// </summary>
    /// <param name="itemOne">Item one.</param>
    /// <param name="itemTwo">Item two.</param>
    public void FillRewardUI(ItemController itemOne, ItemController itemTwo)
    {
        _rewardUI.FillRewardUI(itemOne, itemTwo);
    }

    /// <summary>
    /// Clears reward UI.
    /// </summary>
    public void ClearRewardUI()
    {
        _rewardUI.ClearRewardUI();
    }

    /// <summary>
    /// Shows the info of an enemy.
    /// </summary>
    /// <param name="enemy">Enemy to show info for.</param>
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

    /// <summary>
    /// Updates the Mana UI
    /// </summary>
    /// <param name="mana">Current mana.</param>
    /// <param name="maxMana">Maximum mana.</param>
    public void UpdateManaUI(int mana, int maxMana)
    {
        _statsUI.UpdateManaUI(mana, maxMana);
    }

    /// <summary>
    /// Updates the coin UI.
    /// </summary>
    /// <param name="coins">Current coins.</param>
    public void UpdateCoinsUI(int coins)
    {
        _statsUI.UpdateCoinsUI(coins);
    }

    /// <summary>
    /// Sets activity of selection prompts.
    /// </summary>
    /// <param name="isOn">Activity of selection prompts.</param>
    public void ToggleSelectionPrompts(bool isOn)
    {
        _inputUI.ToggleSelectionPrompts(isOn);
    }

    /// <summary>
    /// Sets activity of damage visual.
    /// </summary>
    /// <param name="isOn">Activity of damage visual.</param>
    public void ToggleDamageVisual(bool isOn)
    {
        _damageTakenVisual.SetActive(isOn);
    }

    /// <summary>
    /// Toggles On/Off prompt of an input.
    /// </summary>
    /// <param name="type">Type of input.</param>
    public void ToggleInputPrompt(Input type)
    {
        _inputUI.ToggleInputPrompt(type);
    }

    /// <summary>
    /// Resets input prompt.
    /// </summary>
    public void ResetInputPrompt()
    {
        _inputUI.ResetInputPrompt();
    }

    /// <summary>
    /// Sets activity of card menu input prompt.
    /// </summary>
    /// <param name="isOn">Activity of card menu prompt.</param>
    public void ToggleCardMenuPrompt(bool isOn)
    {
        _inputUI.ToggleCardMenuPrompt(isOn);
    }

    /// <summary>
    /// Toggles the death screen.
    /// </summary>
    public void ToggleDeathText()
    {
        _deathScreen.SetActive(!_deathScreen.activeSelf);
    }
}
