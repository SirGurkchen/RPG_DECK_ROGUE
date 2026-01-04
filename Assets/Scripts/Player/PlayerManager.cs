using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private PlayerTargeting _targeting;
    [SerializeField] private EnemyBoard _board;
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerCardStash _cards;

    private bool _isChoosingCard = false;

    public event Action OnPlayerTurnEnded;
    public event Action<ItemController> OnItemSelected;
    public event Action<CardController> OnCardSelected;
    public event Action<CardController> OnCardUse;
    public event Action<int> OnRewardConfirm;

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
            _stats.OnPlayerDeath += PlayerDead;
        }
    }

    private void RewardSelected(int rewardIndex)
    {
        OnRewardConfirm?.Invoke(rewardIndex);
    }

    private void HandleCardSwitch()
    {
        _isChoosingCard = !_isChoosingCard;
        Debug.Log(_isChoosingCard);
    }

    private void PlayerDead()
    {
        Debug.Log("Dead!");
    }

    private void Input_OnEnemyRightSelect()
    {
        _targeting.SetSelectTarget(_board.GetMostRighternEnemy());
    }

    private void Input_OnEnemyLeftSelect()
    {
        _targeting.SetSelectTarget(_board.GetMostLefternEnemy());
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
        }

        if (success)
        {
            OnPlayerTurnEnded?.Invoke();
            _targeting.DeselectAll();
        }
    }

    public void TakeDamage(int damage)
    {
        _combat.TakeDamage(_stats, damage);
    }

    public EnemyController GetTargetedEnemy()
    {
        return _targeting.GetCurrentTarget();
    }

    public PlayerInventory GetPlayerInventory()
    {
        return _inventory;
    }

    public PlayerStats GetPlayerStats()
    {
        return _stats;
    }

    public void GiveCard(CardController newCard)
    {
        _cards.AddCardToStash(newCard);
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
        _stats.OnPlayerDeath -= PlayerDead;
    }

    private void OnDestroy()
    {
        OnPlayerTurnEnded = null;
        OnItemSelected = null;
        OnCardUse = null;
        OnCardSelected = null;
        OnRewardConfirm = null;
    }
}
