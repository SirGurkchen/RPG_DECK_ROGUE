using System;
using UnityEngine;

/// <summary>
/// Orchestrates the player and its classes.
/// Most classes that interact with the player do so through this class.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private PlayerTargeting _targeting;
    [SerializeField] private EnemyBoard _board;
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerCardStash _cards;

    private bool _isChoosingCard = false;

    public PlayerStats GetPlayerStats() => _stats;
    public PlayerInventory GetPlayerInventory() => _inventory;
    public EnemyController GetTargetedEnemy() => _targeting.GetCurrentTarget();
    public PlayerTargeting GetPlayerTargeting() => _targeting;
    public PlayerCardStash GetCardStash() => _cards;

    public event Action OnPlayerTurnEnded;
    public event Action<ItemController> OnItemSelected;
    public event Action<CardController> OnCardSelected;
    public event Action<CardController> OnCardUse;
    public event Action<int> OnRewardSelect;
    public event Action OnRewardConfirm;
    public event Action<int> OnShopSelect;
    public event Action OnShopConfirm;
    public event Action OnPlayerDied;
    public event Action OnPlayerCardSwitch;
    public event Action OnPlayerCancel;
    public event Action OnPlayerPaused;

    private void Start()
    {
        var input = GetComponent<PlayerInput>();

        if (input != null )
        {
            input.OnConfirm += HandleConfirm;
            input.OnEnemyLeftSelect += Input_OnEnemyLeftSelect;
            input.OnEnemyRightSelect += Input_OnEnemyRightSelect;
            input.OnItemSelect += Input_OnItemSelect;
            input.OnCardSwitch += HandleCardSwitch;
            input.OnRewardSelect += RewardSelected;
            input.OnRewardConfirm += RewardConfirmed;
            input.OnShopConfirmation += ShopConfirm;
            input.OnShopSelection += ShopSelect;
            input.OnPlayerCancel += PlayerCancel;
            input.OnPlayerPause += PlayerPaused;
            _stats.OnPlayerDeath += PlayerDead;
        }
    }

    private void PlayerPaused()
    {
        OnPlayerPaused?.Invoke();
    }

    private void PlayerCancel()
    {
        OnPlayerCancel?.Invoke();
    }

    private void ShopConfirm()
    {
        OnShopConfirm?.Invoke();
    }

    private void ShopSelect(int index)
    {
        OnShopSelect?.Invoke(index);
    }

    private void RewardConfirmed()
    {
        OnRewardConfirm?.Invoke();
    }

    private void RewardSelected(int rewardIndex)
    {
        OnRewardSelect?.Invoke(rewardIndex);
    }

    private void HandleCardSwitch()
    {
        if (_cards.GetCardCount() != 0)
        {
            _isChoosingCard = !_isChoosingCard;
            OnPlayerCardSwitch?.Invoke();
            if (!_isChoosingCard)
            {
                _cards.DeselectCard();
            }
        }
    }

    private void PlayerDead()
    {
        OnPlayerDied?.Invoke();
    }

    private void Input_OnEnemyRightSelect()
    {
        _targeting.SetSelectTarget(_board.GetMostRighternEnemy(), Input.E);
    }

    private void Input_OnEnemyLeftSelect()
    {
        _targeting.SetSelectTarget(_board.GetMostLefternEnemy(), Input.Q);
    }

    private void Input_OnItemSelect(int item_index)
    {
        if (!_isChoosingCard)
        {
            _inventory.SetEquippedItem(item_index);
            OnItemSelected?.Invoke(_inventory.GetEquippedItem());
        }
        else
        {
            if (_cards.GetCardCount() <= item_index) return;
            bool cardSelect = _cards.SetSelectedCard(item_index);

            if (cardSelect)
            {
                OnCardSelected?.Invoke(_cards.GetEquippedCard());
            }
            else if (_cards.GetEquippedCard() == null)
            {
                OnCardSelected?.Invoke(null);
            }
        }
    }

    public void DeselectAllItems()
    {
        _inventory.DeselectItem();
    }

    private void HandleConfirm()
    {
        bool success = false;

        if (!_isChoosingCard)
        {
            success = _combat.Use(_stats, _targeting.GetCurrentTarget());
        }
        else
        {
            OnCardUse?.Invoke(_cards.GetEquippedCard());
            _cards.CardUsed();
            _isChoosingCard = false;
            _targeting.DeselectAll();
            success = true;
        }

        if (success)
        {
            OnPlayerTurnEnded?.Invoke();
            _targeting.DeselectAll();
        }
        else
        {
            AudioManager.Instance.PlayErrorSound();
        }
    }

    public void TakeDamage(int damage)
    {
        _combat.TakeDamage(_stats, damage);
    }

    public void TryGiveCard(CardController newCard)
    {
        _cards.AddCardToStash(newCard);
    }

    public bool CanAddCard()
    {
        return _cards.CanAdd();
    }

    private void OnDisable()
    {
        var input = GetComponent<PlayerInput>();

        input.OnConfirm -= HandleConfirm;
        input.OnEnemyLeftSelect -= Input_OnEnemyLeftSelect;
        input.OnEnemyRightSelect -= Input_OnEnemyRightSelect;
        input.OnItemSelect -= Input_OnItemSelect;
        input.OnCardSwitch -= HandleCardSwitch;
        input.OnRewardSelect -= RewardSelected;
        input.OnRewardConfirm -= RewardConfirmed;
        _stats.OnPlayerDeath -= PlayerDead;
        input.OnShopConfirmation -= ShopConfirm;
        input.OnShopSelection -= ShopSelect;
        input.OnPlayerCancel -= PlayerCancel;
        input.OnPlayerPause -= PlayerPaused;
    }

    private void OnDestroy()
    {
        OnPlayerTurnEnded = null;
        OnItemSelected = null;
        OnCardUse = null;
        OnCardSelected = null;
        OnRewardSelect = null;
        OnShopConfirm = null;
        OnShopSelect = null;
        OnPlayerCardSwitch = null;
        OnPlayerCancel = null;
        OnPlayerPaused = null;
    }
}
