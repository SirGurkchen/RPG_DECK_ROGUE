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
        _player.OnRewardConfirm += HandleRewardSelection;
        _enemyBoard.OnBoardClear += BoardClear;

        _UIManager.UpdateHealthText(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
        AddCardToPlayer(_testCard);
        AddCardToPlayer(_testCard);
        AddCardToPlayer(_testCard);
        AddCardToPlayer(_testCard);
        GivePlayerItem(_itemDatabase.GetRandomItem());
    }

    private void HandleRewardSelection(int rewardIndex)
    {
        ItemController reward = _rewardManager.GetRewardItem(rewardIndex);
        GivePlayerItem(Instantiate(reward));
        _rewardManager.ClearRewards();
        _UIManager.ClearRewardUI();
        var input = _player.GetComponent<PlayerInput>();
        input.EnablePlayerControls();
        _hordeLogic.RefillBoard();
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
        CardController newCard = Instantiate(card);
        _player.GiveCard(newCard);
        _UIManager.AddCardUI(newCard);
    }

    private void PlayerCardUse(CardController card)
    {
        if (card == null) return;
        _cardManager.PlayCard(card, _player, _player.GetPlayerInventory(), _enemyBoard);
        _UIManager.RemoveCardUI(card);
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
    }

    private void PlayerItemSelect(ItemController item)
    {
        if (item == null)
        {
            Debug.Log("No Item");
            return;
        }
        else
        {
            _UIManager.MarkSelecteItem(_player.GetPlayerInventory().GetInventory().IndexOf(item));
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
        _enemyBoard.OnBoardClear -= BoardClear;
    }
}
