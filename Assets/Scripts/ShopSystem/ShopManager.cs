using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private List<ItemBase> _availableItemCards;
    private ItemBase _availableCard;
    private ItemController _availableItemOne;
    private ItemController _availableItemTwo;

    private void Start()
    {
        CardUnlockManager.Instance.OnLoadFinished += UpdateAvailableCards;
        CardUnlockManager.Instance.OnNewCardUnlock += UpdateAvailableCards;
    }

    public void UpdateAvailableCards(List<ItemBase> unlockedCards)
    {
        _availableItemCards = unlockedCards;
    }

    public void FillShop()
    {
        _availableItemOne = ItemsDataBase.Instance.GetRandomItem();
        if (_availableItemCards.Count <= 0)
        {
            _availableItemTwo = ItemsDataBase.Instance.GetRandomItem();
        }
        else
        {
            _availableCard = _availableItemCards[Random.Range(0, _availableItemCards.Count)];
        }

        Debug.Log("Item 1: " + _availableItemOne + "; Item 2: " + _availableItemTwo + "; Card: " + _availableCard);
    }

    public void EmptyShop()
    {
        _availableItemOne = null;
        _availableItemTwo = null;
        _availableCard = null;
    }

    private void OnDisable()
    {
        CardUnlockManager.Instance.OnLoadFinished -= UpdateAvailableCards;
        CardUnlockManager.Instance.OnNewCardUnlock -= UpdateAvailableCards;
    }
}
