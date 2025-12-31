using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private PlayerTargeting _targeting;
    [SerializeField] private EnemyBoard _board;
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private PlayerInventory _inventory;

    public event Action OnPlayerTurnEnded;
    public event Action<ItemController> OnItemSelected;

    private void Start()
    {
        var input = GetComponent<PlayerInput>();

        if (input != null )
        {
            input.OnConfirm += HandleConfirm;
            input.OnEnemyLeftSelect += Input_OnEnemyLeftSelect;
            input.OnEnemyRightSelect += Input_OnEnemyRightSelect;
            input.OnItemSelect += Input_OnItemSelect;
            _stats.OnPlayerDeath += PlayerDead;
        }
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
        _inventory.SetEquippedItem(item_index);
        OnItemSelected?.Invoke(_inventory.GetEquippedItem());
    }

    public void DeselectAllItems()
    {
        _inventory.DeselectItem();
    }

    private void HandleConfirm()
    {
        bool success = _combat.Use(_stats, _targeting.GetCurrentTarget());

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

    private void OnDisable()
    {
        var input = GetComponent<PlayerInput>();

        input.OnConfirm -= HandleConfirm;
        input.OnEnemyLeftSelect -= Input_OnEnemyLeftSelect;
        input.OnEnemyRightSelect -= Input_OnEnemyRightSelect;
        input.OnItemSelect -= Input_OnItemSelect;
        _stats.OnPlayerDeath -= PlayerDead;
    }

    private void OnDestroy()
    {
        OnPlayerTurnEnded = null;
        OnItemSelected = null;
    }
}
