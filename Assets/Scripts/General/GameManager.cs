using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Orchestrates the 'flow' of the game.
/// Handles simultaneous UI, logic and Game Input changes.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private EnemyBoard _enemyBoard;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private CardController _testCard;
    [SerializeField] private RoundManager _roundManager;

    private void Start()
    {
        _player.OnPlayerTurnEnded += PlayerTurnEnd;
        _player.OnItemSelected += PlayerItemSelect;
        _player.OnCardUse += PlayerCardUse;
        _player.OnCardSelected += PlayerCardSelection;
        _player.OnRewardSelect += HandleRewardSelection;
        _player.OnRewardConfirm += HandleRewardConfirm;
        _player.GetPlayerInventory().OnCardWasUnlocked += HandleCardUnlock;
        _player.GetPlayerTargeting().OnEnemyTargeted += HandleEnemyTargeted;
        _player.GetPlayerStats().OnPlayerHeal += HandlePlayerHeal;
        _player.GetPlayerStats().OnManaChange += HandleManaChange;
        _enemyBoard.OnBoardClear += BoardClear;
        _enemyBoard.OnEnemyKilled += HandleCoinsGain;
        CardUnlockManager.Instance.OnLoadFinished += GivePlayerUnlockedCards;

        _player.GetPlayerInventory().GetInventory()[4] = Instantiate(ItemsDataBase.Instance.GetItemByName("Hammer"));
        _UIManager.UpdateHealthText(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
        _UIManager.UpdateManaUI(_player.GetPlayerStats().Mana, _player.GetPlayerStats().MaxMana);
        _UIManager.UpdateCoinsUI(_player.GetPlayerStats().Coins);
        _UIManager.UpdateWeaponUI(_player.GetPlayerInventory().GetInventory());
    }

    private void HandleManaChange()
    {
        _UIManager.UpdateManaUI(_player.GetPlayerStats().Mana, _player.GetPlayerStats().MaxMana);
    }

    private void HandlePlayerHeal()
    {
        _UIManager.UpdateHealthText(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
    }

    private void HandleCoinsGain(int coins)
    {
        _player.GetPlayerStats().AddCoins(coins);
        _UIManager.UpdateCoinsUI(_player.GetPlayerStats().Coins);
    }

    private void HandleEnemyTargeted(EnemyController enemy)
    {
        _UIManager.ShowEnemyInfo(enemy);
    }

    private void HandleCardUnlock(CardController card, ItemController item)
    {        
        if (!item.GetItemBase().IsNotReward && item.GetItemBase().UnlockCard != null)
        {
            CardUnlockManager.Instance.SetCardUnlocked(item.GetItemBase());
            if (_player.CanAddCard())
            {
                AddCardToPlayer(card);
            }
        }
        else
        {
            Debug.Log("No Card Unlocked!");
        }
    }

    private void GivePlayerUnlockedCards(List<ItemBase> items)
    {
        foreach (ItemBase item in items)
        {
            AddCardToPlayer(item.UnlockCard);
        }
    }

    private void HandleRewardConfirm()
    {
        _roundManager.HandleRewardConfirm(_player, _UIManager);
    }

    private void HandleRewardSelection(int rewardIndex)
    {
        _roundManager.HandleRewardSelection(rewardIndex, _UIManager);
    }

    private void BoardClear()
    {
        _roundManager.BoardClear(_player, _UIManager);
    }

    private void PlayerCardSelection(CardController card)
    {
        if (card == null) return;
        card.SelectCard();
    }

    private void AddCardToPlayer(CardController card)
    {
        if (_player.CanAddCard())
        {
            CardController newCard = Instantiate(card);
            _player.TryGiveCard(newCard);
            _UIManager.AddCardUI(newCard);
        }
    }

    private void PlayerCardUse(CardController card)
    {
        if (card == null) return;
        _cardManager.PlayCard(card, _player, _player.GetPlayerInventory(), _enemyBoard);
        _UIManager.RemoveCardUI(card);
        _UIManager.DemarkItems();
        _UIManager.RemoveItemDescription();
        _UIManager.ShowEnemyInfo(null);
    }

    private void PlayerTurnEnd()
    {
        _roundManager.StartBufferedRound(_UIManager, _player, _enemyBoard);
    }

    private void PlayerItemSelect(ItemController item)
    {
        if (item == null)
        {
            _UIManager.RemoveItemDescription();
            return;
        }
        else
        {
            _UIManager.MarkSelectItem(_player.GetPlayerInventory().GetInventory().IndexOf(item));
            _UIManager.ShowItemDescription(item);
        }
    }

    private void GivePlayerItem(ItemController item)
    {
        if (_player.GetPlayerInventory().CanAddItem())
        {
            _player.GetPlayerInventory().GiveItemToInventory(Instantiate(item));
            _UIManager.UpdateWeaponUI(_player.GetPlayerInventory().GetInventory());
        }
    }

    private void OnDisable()
    {
        _player.OnPlayerTurnEnded -= PlayerTurnEnd;
        _player.OnItemSelected -= PlayerItemSelect;
        _player.OnCardUse -= PlayerCardUse;
        _player.OnCardSelected -= PlayerCardSelection;
        _player.OnRewardSelect -= HandleRewardSelection;
        _player.OnRewardConfirm -= HandleRewardConfirm;
        _player.GetPlayerInventory().OnCardWasUnlocked -= HandleCardUnlock;
        _player.GetPlayerTargeting().OnEnemyTargeted -= HandleEnemyTargeted;
        _player.GetPlayerStats().OnPlayerHeal -= HandlePlayerHeal;
        _enemyBoard.OnBoardClear -= BoardClear;
        _enemyBoard.OnEnemyKilled -= HandleCoinsGain;
        CardUnlockManager.Instance.OnLoadFinished -= GivePlayerUnlockedCards;
    }
}
