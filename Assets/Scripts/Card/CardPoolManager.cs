using System.Collections.Generic;
using UnityEngine;

public class CardPoolManager : MonoBehaviour
{
    public static CardPoolManager Instance { get; private set; }

    private List<CardController> _unlockedCards;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There are multiple Card Pool Managers!");
        }
        Instance = this; 
    }


    public void SetUnlockedCards(List<ItemBase> items)
    {
        foreach (ItemBase itemUnlock in items)
        {
            _unlockedCards.Add(itemUnlock.UnlockCard);
        }
    }

    public List<CardController> GetUnlockedCards()
    {
        return _unlockedCards;
    }
}
