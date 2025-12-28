using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private EnemyController _selectTarget;

    public event Action OnEnemyLeftSelect;
    public event Action OnEnemyRightSelect;

    private void Start()
    {
        GameInput.Instance.OnConfirmPress += Instance_OnConfirmPress;
        GameInput.Instance.OnSelectLeftPress += Instance_OnSelectLeftPress;
        GameInput.Instance.OnSelectRightPress += Instance_OnSelectRightPress;
    }

    private void Instance_OnSelectLeftPress()
    {
        OnEnemyLeftSelect?.Invoke();
    }

    private void Instance_OnSelectRightPress()
    {
        OnEnemyRightSelect?.Invoke();
    }

    private void Instance_OnConfirmPress()
    {
        if (_selectTarget != null)
        {
            UseCurrentlySelectedItem(_selectTarget);
            Debug.Log(_selectTarget.GetEnemyStats());
        }
        else
        {
            UseCurrentlySelectedItem();
        }
    }

    public void UseCurrentlySelectedItem(EnemyController target = null)
    {
        if (_inventory != null)
        {
            _inventory.GetEquippedItem()?.Use(_stats, target);
        }
    }

    public void GiveItemToPlayer(ItemBase new_item)
    {
        if (_inventory != null)
        {
            _inventory.GiveItemToInventory(new_item);
        }
        else
        {
            print("Item could not be added to the Inventory!");
        }
    }

    public void EquipItem(int inventory_index)
    {
        if (_inventory.GetItemAtInvetory(inventory_index) != null)
        {
            _inventory.SetEquippedItem(_inventory.GetItemAtInvetory(inventory_index));
        }
    }

    public void SetSelectTarget(EnemyController enemy)
    {
        _selectTarget = enemy;
    }

    private void OnDisable()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnConfirmPress -= Instance_OnConfirmPress;
            GameInput.Instance.OnSelectRightPress -= Instance_OnSelectRightPress;
            GameInput.Instance.OnSelectLeftPress -= Instance_OnSelectLeftPress;
        }
    }

    private void OnDestroy()
    {
        OnEnemyLeftSelect = null;
        OnEnemyRightSelect = null;
    }
}
