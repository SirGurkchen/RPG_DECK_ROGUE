using System.Collections.Generic;
using UnityEngine;

public class CardUnlockManager : MonoBehaviour, IDataPersistence
{
    public static CardUnlockManager Instance { get; private set; }

    private List<ItemBase> _unlockedItemsCards;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There are multiple Card Unlock Managers!");
        }
        Instance = this; 
    }

    public void SetCardUnlocked(ItemBase unlockItem)
    {
        if (IsCardUnlcoked(unlockItem.ItemName))
        {
            return;
        }
        _unlockedItemsCards.Add(unlockItem);
        Debug.Log("Unlocked Card: " + unlockItem.UnlockCard.GetCard().Name);
    }

    public bool IsCardUnlcoked(string itemName)
    {
        if (_unlockedItemsCards.Count == 0)
        {
            return false;
        }

        foreach(ItemBase unlockable in _unlockedItemsCards)
        {
            if (unlockable.ItemName == itemName)
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
            foreach(ItemBase item in data.unlockedCards)
            {
                Debug.Log(item.ItemName);
            }
            _unlockedItemsCards = data.unlockedCards;
        }
    }
    public void SaveData(ref GameData data)
    {
        data.unlockedCards = _unlockedItemsCards;
    }
}
