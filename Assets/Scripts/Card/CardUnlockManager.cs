using System;
using System.Collections.Generic;
using UnityEngine;

public class CardUnlockManager : MonoBehaviour, IDataPersistence
{
    public static CardUnlockManager Instance { get; private set; }

    private List<string> _unlockedItemsCards;

    public event Action<List<string>> OnLoadFinished;
    public event Action<List<string>> OnNewCardUnlock;

    public List<string> UnlockedItemsCards => _unlockedItemsCards;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There are multiple Card Unlock Managers!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _unlockedItemsCards = new List<string>(); 
    }

    private void OnEnable()
    {
        DataPersistenceManager.Instance.Register(this);
    }

    public void SetCardUnlocked(ItemBase unlockItem)
    {
        if (IsCardUnlcoked(unlockItem.ItemName))
        {
            return;
        }
        _unlockedItemsCards.Add(unlockItem.ItemName);
        DataPersistenceManager.Instance.SaveNewCardUnlock();
        OnNewCardUnlock?.Invoke(_unlockedItemsCards);
    }

    public bool IsCardUnlcoked(string itemName)
    {
        if (_unlockedItemsCards.Count == 0)
        {
            return false;
        }

        foreach(string unlockable in _unlockedItemsCards)
        {
            if (unlockable == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public void LoadData(GameData data)
    {
        if (data != null)
        {
            _unlockedItemsCards = data.unlockedCards;
            OnLoadFinished?.Invoke(_unlockedItemsCards);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.unlockedCards = _unlockedItemsCards;
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance?.Unregister(this);
    }
}
