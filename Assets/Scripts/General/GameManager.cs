using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private AudioClip _deathSound;

    private bool isPaused = false;

    private void Start()
    {
        LoadingScreenManager.Instance.OnLoadingScreenFinished += LoadingFinished;
        _player.GetPlayerInventory().GetInventory()[4] = Instantiate(ItemsDataBase.Instance.GetItemByName("Hammer"));
        _UIManager.UpdateHealthBar(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
        _UIManager.UpdateManaUI(_player.GetPlayerStats().Mana, _player.GetPlayerStats().MaxMana);
        _UIManager.UpdateCoinsUI(_player.GetPlayerStats().Coins);
        _UIManager.UpdateWeaponUI(_player.GetPlayerInventory().GetInventory());
    }

    private void LoadingFinished()
    {
        LoadingScreenManager.Instance.OnLoadingScreenFinished -= LoadingFinished;

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
        _player.OnShopConfirm += HandleShopConfirm;
        _player.OnShopSelect += HandleShopSelect;
        _player.OnPlayerDied += HandlePlayerDeath;
        _player.OnPlayerCardSwitch += HandlePlayerCardSwitch;
        _player.OnPlayerCancel += HandlePlayerCancel;
        _player.OnPlayerPaused += HandlePlayerPause;
        _enemyBoard.OnBoardClear += BoardClear;
        _enemyBoard.OnEnemyKilled += HandleCoinsGain;

        GameInput.Instance.ChangePlayerActive(true);
        StartFirstRound();
    }

    private void HandlePlayerPause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        isPaused = !isPaused;
        GameInput.Instance.ChangePlayerActive(!isPaused);
        _UIManager.TogglePauseMenu(isPaused);
    }

    private void HandlePlayerCancel()
    {
        _roundManager.CancelRoundBuffer(_UIManager);
    }

    private void HandlePlayerCardSwitch()
    {
        _UIManager.ToggleInputPrompt(Input.Control);
    }

    private void HandlePlayerDeath()
    {
        StartCoroutine(WaitTillPlayerDead());
    }

    private IEnumerator WaitTillPlayerDead()
    {
        yield return new WaitUntil(() => GameInput.Instance.IsInputActive());
        _UIManager.ToggleDeathText();
        GameInput.Instance.ChangePlayerActive(false);
        AudioManager.Instance.PlayAudioClip(_deathSound);
        yield return new WaitUntil(() => AudioManager.Instance.IsSoundFinished());
        LoadingScreenManager.Instance.PlayLoadAnimation("MainMenu");
    }

    private void StartFirstRound()
    {
        _roundManager.StartPredeterminedRound("Bandit");
    }

    private void HandleManaChange()
    {
        _UIManager.UpdateManaUI(_player.GetPlayerStats().Mana, _player.GetPlayerStats().MaxMana);
    }

    private void HandleShopConfirm()
    {
        _roundManager.HandleShopConfirmation(_player, _UIManager);
    }

    private void HandleShopSelect(int index)
    {
        _roundManager.HandleShopSelect(index, _UIManager);
    }

    private void HandlePlayerHeal()
    {
        _UIManager.UpdateHealthBar(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
    }

    private void HandleCoinsGain(int coins)
    {
        _player.GetPlayerStats().AddCoins(coins);
        _UIManager.UpdateCoinsUI(_player.GetPlayerStats().Coins);
    }

    private void HandleEnemyTargeted(EnemyController enemy, Input input)
    {
        if (GameInput.Instance.IsInputActive())
        {
            _UIManager.ShowEnemyInfo(enemy);
            _UIManager.ToggleInputPrompt(input);
        }
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
            _UIManager.ToggleCardMenuPrompt(true);
        }
    }

    private void PlayerCardUse(CardController card)
    {
        if (card == null) return;
        _cardManager.PlayCard(card, _player, _player.GetPlayerInventory(), _enemyBoard);
        _UIManager.RemoveCardUI(card);
        _UIManager.RemoveItemDescription();
        _UIManager.ShowEnemyInfo(null);
        _UIManager.ToggleInputPrompt(Input.Control);
        if (_player.GetCardStash().GetCardCount() - 1 <= 0) // Minus 1 because card stash Count has not been updated at this point, so the used card has to be subtracted manually.
        {
            _UIManager.ToggleCardMenuPrompt(false);
        }
    }

    private void PlayerTurnEnd()
    {
        DeactivatePlayer();
        if (_enemyBoard.GetEnemies().Count == 0) return;
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
            _UIManager.ShowItemDescription(item);
        }
    }

    private void DeactivatePlayer()
    {
        _UIManager.RemoveItemDescription();
        _UIManager.ShowEnemyInfo(null);
        _UIManager.ResetInputPrompt();
        _player.DeselectAllItems();
        _UIManager.UpdateWeaponUI(_player.GetPlayerInventory().GetInventory());
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
        _player.OnShopConfirm -= HandleShopConfirm;
        _player.OnShopSelect -= HandleShopSelect;
        _player.OnPlayerDied -= HandlePlayerDeath;
        _player.OnPlayerCardSwitch -= HandlePlayerCardSwitch;
        _player.OnPlayerCancel -= HandlePlayerCancel;
        LoadingScreenManager.Instance.OnLoadingScreenFinished -= LoadingFinished;
    }
}
