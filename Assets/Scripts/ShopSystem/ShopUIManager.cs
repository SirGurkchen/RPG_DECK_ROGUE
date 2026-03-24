using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the UI presentation of the Shop.
/// </summary>
public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private Image[] _shopItems;
    [SerializeField] private GameObject _cardPlacement;
    [SerializeField] private TextMeshProUGUI _priceOne;
    [SerializeField] private TextMeshProUGUI _priceTwo;

    private CardController _currentCard;

    /// <summary>
    /// Fills shop UI with items only.
    /// </summary>
    /// <param name="itemOne">Item one.</param>
    /// <param name="itemTwo">Item two.</param>
    public void FillShopUIItemsOnly(ItemController itemOne, ItemController itemTwo)
    {
        _shopItems[0].sprite = itemOne.GetItemBase().Icon;
        _shopItems[1].sprite = itemTwo.GetItemBase().Icon;

        _priceOne.text = "1: $" + itemOne.GetItemBase().Price;
        _priceTwo.text = "$" + itemTwo.GetItemBase().Price + ": 2";

        foreach (Image item in _shopItems)
        {
            item.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Fills hop UI with one item and one card.
    /// </summary>
    /// <param name="item">Item one.</param>
    /// <param name="card">Card one.</param>
    public void FillShopUI(ItemController item, CardController card)
    {
        _shopItems[0].sprite = item.GetItemBase().Icon;
        _shopItems[0].gameObject.SetActive(true);

        _priceOne.text = "$" + item.GetItemBase().Price;
        _priceTwo.text = "$" + card.GetCard().ShopPrice;

        CardController shopCard = Instantiate(card);
        _currentCard = shopCard;
        shopCard.gameObject.transform.SetParent(_cardPlacement.transform);
        shopCard.gameObject.transform.position = _cardPlacement.transform.position;
    }

    /// <summary>
    /// Clears the shop UI.
    /// </summary>
    public void ClearShopUI()
    {
        DemarkAllShopItems();
        foreach (Image item in _shopItems)
        {
            item.gameObject.SetActive(false);
        }

        if (_currentCard != null)
        {
            Destroy(_currentCard.gameObject);
            _currentCard = null;
        }
        _shopItems[0].sprite = null;
        _shopItems[1].sprite = null;

        _priceOne.text = string.Empty;
        _priceTwo.text = string.Empty;
    }

    /// <summary>
    /// Demarks all shop items.
    /// </summary>
    public void DemarkAllShopItems()
    {
        foreach (Image item in _shopItems)
        {
            item.color = Color.white;
        }

        if (_currentCard != null)
        {
            _currentCard.ToggleShopSelection(false);
        }
    }

    /// <summary>
    /// Marks a shop item.
    /// </summary>
    /// <param name="index">Index of item to be marked.</param>
    public void MarkShopItem(int index)
    {
        DemarkAllShopItems();
        _shopItems[index].color = Color.red;
    }

    /// <summary>
    /// Marks the shop card.
    /// </summary>
    public void MarkShopCard()
    {
        DemarkAllShopItems();
        _currentCard.ToggleShopSelection(true);
    }
}
