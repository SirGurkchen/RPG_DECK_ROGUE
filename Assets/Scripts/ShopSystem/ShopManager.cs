using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    private List<ItemBase> _availableItemCards;

    private void Start()
    {
        CardUnlockManager.Instance.OnLoadFinished += UpdateAvailableCards;
        CardUnlockManager.Instance.OnNewCardUnlock += UpdateAvailableCards;
    }

    public void UpdateAvailableCards(List<ItemBase> unlockedCards)
    {
        _availableItemCards = unlockedCards;
    }

    private void OnDisable()
    {
        CardUnlockManager.Instance.OnLoadFinished -= UpdateAvailableCards;
        CardUnlockManager.Instance.OnNewCardUnlock -= UpdateAvailableCards;
    }
}
