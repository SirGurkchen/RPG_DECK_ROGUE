using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private Image[] _shopItems;
    [SerializeField] private GameObject _cardPlacement;
    [SerializeField] private TextMeshProUGUI _priceOne;
    [SerializeField] private TextMeshProUGUI _priceTwo;

    private CardController _currentCard;

    public void FillShopUIItemsOnly(ItemController itemOne, ItemController itemTwo)
    {
        _shopItems[0].sprite = itemOne.GetItemBase().Icon;
        _shopItems[1].sprite = itemTwo.GetItemBase().Icon;

        _priceOne.text = "$" + itemOne.GetItemBase().Price;
        _priceTwo.text = "$" + itemTwo.GetItemBase().Price;

        foreach (Image item in _shopItems)
        {
            item.gameObject.SetActive(true);
        }
    }

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

    public void MarkShopItem(int index)
    {
        DemarkAllShopItems();
        _shopItems[index].color = Color.red;
    }

    public void MarkShopCard()
    {
        DemarkAllShopItems();
        _currentCard.ToggleShopSelection(true);
    }
}
