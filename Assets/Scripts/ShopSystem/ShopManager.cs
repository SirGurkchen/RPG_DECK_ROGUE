using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logic of the Shop.
/// This includes Item selection of trying to buy selecting items.
/// </summary>
public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopUIManager _shopUI;
    [SerializeField] private ShopAnimator _shopAnimator;

    private List<ItemBase> _availableItemCards;
    private List<ItemController> _availableItems;
    private ItemBase _availableCard;

    private bool _itemOnly = true;
    private ItemController _selectItem;
    private ItemBase _selectCard;

    private void Awake()
    {
        _availableItemCards = new List<ItemBase>();
        _availableItems = new List<ItemController>();
    }

    private void Start()
    {
        UpdateAvailableCards(CardUnlockManager.Instance.UnlockedItemsCards);
    }

    /// <summary>
    /// Updates the shop with new available cards.
    /// </summary>
    /// <param name="unlockedCards">List of unlocked cards.</param>
    public void UpdateAvailableCards(List<string> unlockedCards)
    {
        _availableItemCards.Clear();

        foreach (string itemName in unlockedCards)
        {
            ItemController item = ItemsDataBase.Instance.GetItemByName(itemName);
            if (item == null)
            {
                Debug.Log("No Item found while updating shop!");
                continue;
            }
            _availableItemCards.Add(item.GetItemBase());
        }
    }

    /// <summary>
    /// Starts filling the shop.
    /// </summary>
    public void EnterShop()
    {
        StartCoroutine(FillShop());
    }

    private IEnumerator FillShop()
    {
        yield return new WaitUntil(() => AudioManager.Instance.IsSoundFinished());
        Tween enterTween = _shopAnimator.PlayShopEnterAnimation();
        if (enterTween != null)
        {
            yield return enterTween.WaitForCompletion();
        }

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

    /// <summary>
    /// Handles shop item selection.
    /// </summary>
    /// <param name="index">Index of selected item.</param>
    /// <param name="UI">UI.</param>
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

    /// <summary>
    /// Handle shop confirmation.
    /// </summary>
    /// <param name="player">Player main clas.</param>
    /// <param name="UI">UI.</param>
    /// <returns>True if shop purchase was succesful.</returns>
    public bool HandleShopConfirm(PlayerManager player, UIManager UI)
    {
        if (player.GetPlayerInventory().CanAddItem() && _selectItem != null)
        {
            if (_selectItem.GetItemBase().Price > player.GetPlayerStats().Coins)
            {
                AudioManager.Instance.PlayErrorSound();
                return false;
            }

            player.GetPlayerInventory().GiveItemToInventory(Instantiate(_selectItem));
            player.RemoveCoins(_selectItem.GetItemBase().Price);
            UI.UpdateCoinsUI(player.GetPlayerStats().Coins);
            UI.UpdateInventoryUI(player.GetPlayerInventory().GetInventory());
            StartCoroutine(EmptyShop());
        }
        else if (_selectCard != null && player.CanAddCard())
        {
            if (_selectCard.UnlockCard.GetCard().ShopPrice > player.GetPlayerStats().Coins)
            {
                AudioManager.Instance.PlayErrorSound();
                return false;
            }

            CardController newCard = Instantiate(_selectCard.UnlockCard);
            player.TryGiveCard(newCard);
            player.RemoveCoins(_selectCard.UnlockCard.GetCard().ShopPrice);
            UI.UpdateCoinsUI(player.GetPlayerStats().Coins);
            UI.AddCardUI(newCard);
            StartCoroutine(EmptyShop());
        }
        return true;
    }

    /// <summary>
    /// Checks if a reward is selected.
    /// </summary>
    /// <returns>True if a reward is selected.</returns>
    public bool IsRewardSelected()
    {
        return _selectCard != null || _selectItem != null;
    }

    /// <summary>
    /// Empties the shop and starts shop exit animation.
    /// </summary>
    public IEnumerator EmptyShop()
    {
        _shopUI.ClearShopUI();
        _availableItems.Clear();
        _availableCard = null;
        _selectCard = null;
        _selectItem = null;

        Tween exitTween = _shopAnimator.PlayShopLeaveAnimation();
        if (exitTween != null)
        {
            yield return exitTween.WaitForCompletion();
        }
    }

    private void OnDisable()
    {
        CardUnlockManager.Instance.OnLoadFinished -= UpdateAvailableCards;
        CardUnlockManager.Instance.OnNewCardUnlock -= UpdateAvailableCards;
    }
}
