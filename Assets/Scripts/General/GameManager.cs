using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private EnemyBoard _enemyBoard;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private CardController _testCard;
    [SerializeField] private HordeLogic _hordeLogic;
    [SerializeField] private ItemsDataBase _itemDatabase;
    [SerializeField] private RewardManager _rewardManager;

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
        _enemyBoard.OnBoardClear += BoardClear;

        _UIManager.UpdateHealthText(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
        AddCardToPlayer(_testCard);
        AddCardToPlayer(_testCard);
        AddCardToPlayer(_testCard);
        _player.GetPlayerInventory().GetInventory()[4] = _itemDatabase.GetItemByName("Fist");
        _UIManager.UpdateWeaponUI(_player.GetPlayerInventory().GetInventory());
    }

    private void HandlePlayerHeal()
    {
        _UIManager.UpdateHealthText(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
    }

    private void HandleEnemyTargeted(EnemyController enemy)
    {
        _UIManager.ShowEnemyInfo(enemy);
    }

    private void HandleCardUnlock(CardController card, ItemController item)
    {
        if (_player.CanAddCard())
        {
            AddCardToPlayer(card);
            item.GetItemBase().SetUnlocked();
        }
        else
        {
            item.GetItemBase().ResetUnlock();
        }
    }

    private void HandleRewardConfirm()
    {
        GivePlayerItem(Instantiate(_rewardManager.GetSelectReward()));
        _rewardManager.ClearRewards();
        _UIManager.ClearRewardUI();
        _UIManager.RemoveItemDescription();
        var input = _player.GetComponent<PlayerInput>();
        input.EnablePlayerControls();
        _hordeLogic.RefillBoard();
    }

    private void HandleRewardSelection(int rewardIndex)
    {
        _rewardManager.SetSelectReward(rewardIndex);
        _UIManager.ShowRewardItemDescription(_rewardManager.GetSelectReward(), rewardIndex);
    }

    private void BoardClear()
    {
        if (_player.GetPlayerInventory().CanAddItem())
        {
            List<ItemController> rewards = _rewardManager.GetRandomRewardItems(_itemDatabase);
            _UIManager.FillRewardUI(rewards[0], rewards[1]);
            var input = _player.GetComponent<PlayerInput>();
            input.EnableRewardControls();
        }
        else
        {
            _hordeLogic.RefillBoard();
        }
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
        foreach (EnemyController enemy in _enemyBoard.GetEnemies())
        {
            enemy.Attack(_player);
        }

        _player.DeselectAllItems();
        _UIManager.UpdateHealthText(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
        _UIManager.UpdateWeaponUI(_player.GetPlayerInventory().GetInventory());
        _UIManager.RemoveItemDescription();
        _UIManager.ShowEnemyInfo(null);
    }

    private void PlayerItemSelect(ItemController item)
    {
        if (item == null)
        {
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
    }
}
