using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Orchestrates the logic and visuals of an item instance.
/// Script is part of the components of every item.
/// </summary>
public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemBase _itemData;
    [SerializeField] private Image _itemImage;
    private int _currentEndurance;

    public event Action<ItemController> OnItemDestroy;
    public event Action<ItemController> OnItemFirstAddedToInventory;

    private void Awake()
    {
        InitializeItem();
    }

    private void Start()
    {
        if (_itemImage != null && _itemData.Icon != null)
        {
            _itemImage.sprite = _itemData.Icon;
        }
    }

    private void InitializeItem()
    {
        if (_itemData is IDurable item)
        {
            _currentEndurance = item.MaxDurability;
        }
    }

    public bool Use(PlayerStats stats, EnemyController target = null)
    {
        bool suc = _itemData.Use(stats, target);

        if (suc)
        {
            if (_currentEndurance > 0)
            {
                _currentEndurance--;
            }
            CheckDestroy();
        }

        return suc;
    }

    public string GetItemName()
    {
        return _itemData.ItemName;
    }

    public ItemBase GetItemBase()
    {
        return _itemData;
    }

    public int GetCurrentEndurance()
    {
        return _currentEndurance;
    }

    private void CheckDestroy()
    {
        if (_itemData.ItemName == "Hammer") return;

        if (_currentEndurance <= 0)
        {
            OnItemDestroy?.Invoke(this);
            Destroy(this.gameObject);
        }
    }

    public bool CheckCardUnlock()
    {
        if (CardUnlockManager.Instance.IsCardUnlcoked(_itemData.ItemName)) // Card of Item is already unlocked
        {
            return false;
        }
        else if (_itemData.UnlockCard == null) // Item does not have any card to unlock
        {
            return false;
        }

        OnItemFirstAddedToInventory?.Invoke(this);
        return true;
    }

    public Image GetItemIcon()
    {
        return _itemImage;
    }

    private void OnDestroy()
    {
        OnItemDestroy = null;
        OnItemFirstAddedToInventory = null;
    }
}
