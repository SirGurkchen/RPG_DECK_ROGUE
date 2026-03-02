using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopUIManager _shopUI;

    private List<ItemBase> _availableItemCards;
    private List<ItemController> _availableItems;
    private ItemBase _availableCard;

    private bool _itemOnly = true;
    private ItemController _selectItem;
    private ItemBase _selectCard;

    private void Start()
    {
        CardUnlockManager.Instance.OnLoadFinished += UpdateAvailableCards;
        CardUnlockManager.Instance.OnNewCardUnlock += UpdateAvailableCards;
        _availableItemCards = new List<ItemBase>();
        _availableItems = new List<ItemController>();
    }

    public void UpdateAvailableCards(List<ItemBase> unlockedCards)
    {
        _availableItemCards = unlockedCards;
    }

    public void FillShop()
    {
        _availableItems.Add(ItemsDataBase.Instance.GetRandomItem());
        if (_availableItemCards.Count <= 0)
        {
            _availableItems.Add(ItemsDataBase.Instance.GetRandomItem());
            _shopUI.FillShopUIItemsOnly(_availableItems[0], _availableItems[1]);
            _itemOnly = true;
        }
        else
        {
            _availableCard = _availableItemCards[Random.Range(0, _availableItemCards.Count)];
            _shopUI.FillShopUI(_availableItems[0], _availableCard.UnlockCard);
            _itemOnly = false;
        }
    }

    public void HandleShopSelection(int index, UIManager UI)
    {
        if (_itemOnly)
        {
            _selectCard = null;
            _selectItem = _availableItems[index];
            _shopUI.MarkShopItem(index);
        }
        else
        {
            if (index == 0)
            {
                _selectCard = null;
                _selectItem = _availableItems[index];
                _shopUI.MarkShopItem(index);
            }
            else
            {
                _selectItem = null;
                _selectCard = _availableCard;
                _shopUI.MarkShopCard();
            }
        }
    }

    public void HandleShopConfirm(PlayerManager player, UIManager UI)
    {
        if (player.GetPlayerInventory().CanAddItem() && _selectItem != null)
        {
            player.GetPlayerInventory().GiveItemToInventory(Instantiate(_selectItem));
            UI.UpdateWeaponUI(player.GetPlayerInventory().GetInventory());
            EmptyShop();
        }
        else if (_selectCard != null && player.CanAddCard())
        {
            CardController newCard = Instantiate(_selectCard.UnlockCard);
            player.TryGiveCard(newCard);
            UI.AddCardUI(newCard);
            EmptyShop();
        }
    }

    private void EmptyShop()
    {
        _shopUI.ClearShopUI();
        _availableItems.Clear();
        _availableCard = null;
    }

    private void OnDisable()
    {
        CardUnlockManager.Instance.OnLoadFinished -= UpdateAvailableCards;
        CardUnlockManager.Instance.OnNewCardUnlock -= UpdateAvailableCards;
    }
}
