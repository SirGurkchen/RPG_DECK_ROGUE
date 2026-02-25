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
    [SerializeField] private HordeLogic _hordeLogic;
    [SerializeField] private ItemsDataBase _itemDatabase;
    [SerializeField] private RewardManager _rewardManager;

    private const float ROUND_BUFFER_TIMER = 0.75f;
    private Coroutine _roundBufferRoutine;

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

        _UIManager.UpdateHealthText(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
        _UIManager.UpdateManaUI(_player.GetPlayerStats().Mana, _player.GetPlayerStats().MaxMana);
        _player.GetPlayerInventory().GetInventory()[4] = Instantiate(_itemDatabase.GetItemByName("Hammer"));
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

    private void HandleEnemyTargeted(EnemyController enemy)
    {
        _UIManager.ShowEnemyInfo(enemy);
    }

    private void HandleCardUnlock(CardController card, ItemController item)
    {        
        if (!item.GetItemBase().IsNotReward && item.GetItemBase().UnlockCard != null)
        {
            Debug.Log("Unlocked Card!");
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

    private void HandleRewardConfirm()
    {
        GivePlayerItem(_rewardManager.GetSelectReward());
        _rewardManager.ClearRewards();
        _UIManager.ClearRewardUI();
        _UIManager.RemoveItemDescription();
        GameInput.Instance.ChangeRewardActive(false);
        GameInput.Instance.ChangePlayerActive(true);
        _hordeLogic.RefillBoard();
    }

    private void HandleRewardSelection(int rewardIndex)
    {
        _rewardManager.SetSelectReward(rewardIndex);
        _UIManager.ShowRewardItemDescription(_rewardManager.GetSelectReward(), rewardIndex);
    }

    private void BoardClear()
    {
        if (_roundBufferRoutine != null)
        {
            StopCoroutine(_roundBufferRoutine);
            _roundBufferRoutine = null;
        }

        GameInput.Instance.ChangePlayerActive(false);

        if (_player.GetPlayerInventory().CanAddItem())
        {
            List<ItemController> rewards = _rewardManager.GetRandomRewardItems(_itemDatabase);
            _UIManager.FillRewardUI(rewards[0], rewards[1]);
            GameInput.Instance.ChangeRewardActive(true);
        }
        else
        {
            GameInput.Instance.ChangeRewardActive(false);
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
        _roundBufferRoutine = StartCoroutine(StartBufferedRound());
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

    private IEnumerator StartBufferedRound()
    {
        GameInput.Instance.ChangePlayerActive(false);
        _UIManager.RemoveItemDescription();
        _UIManager.ShowEnemyInfo(null);
        _player.DeselectAllItems();
        _UIManager.UpdateWeaponUI(_player.GetPlayerInventory().GetInventory());

        yield return new WaitForSeconds(ROUND_BUFFER_TIMER);

        foreach (EnemyController enemy in _enemyBoard.GetEnemies())
        {
            enemy.Attack(_player);
        }

        _player.GetPlayerStats().AddMana(1);
        _UIManager.UpdateHealthText(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
        _UIManager.UpdateManaUI(_player.GetPlayerStats().Mana, _player.GetPlayerStats().MaxMana);

        if (_enemyBoard.GetEnemies().Count > 0)
        {
            GameInput.Instance.ChangePlayerActive(true);
        }
        _roundBufferRoutine = null;
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
