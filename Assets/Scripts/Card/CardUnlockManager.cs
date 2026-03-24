using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the loading and saving of unlocked cards.
/// Unlocked Cards data comes from DataPersitenceManager.
/// New cards are also saved in that manager.
/// </summary>
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

    /// <summary>
    /// Sets a card as unlocked.
    /// Saves the new unlocked card.
    /// </summary>
    /// <param name="unlockItem">Item which the card has ben unlocked of.</param>
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

    /// <summary>
    /// Checks if the card of an item is unlocked.
    /// </summary>
    /// <param name="itemName">Name of the item to check if the card is unlocked.</param>
    /// <returns>True if the card is unlocked.</returns>
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

    /// <summary>
    /// Loads unlocked cards game data
    /// </summary>
    /// <param name="data">Saved game data of the game.</param>
    public void LoadData(GameData data)
    {
        if (data != null)
        {
            _unlockedItemsCards = data.unlockedCards;
            OnLoadFinished?.Invoke(_unlockedItemsCards);
        }
    }

    /// <summary>
    /// Saves data to the game data.
    /// </summary>
    /// <param name="data">Data to save.</param>
    public void SaveData(ref GameData data)
    {
        data.unlockedCards = _unlockedItemsCards;
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance?.Unregister(this);
    }
}
