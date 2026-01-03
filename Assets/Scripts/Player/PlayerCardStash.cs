using System.Collections.Generic;
using UnityEngine;

public class PlayerCardStash : MonoBehaviour
{
    [SerializeField] private List<CardController> _playerCards;
    [SerializeField] private CardController _selectedCard;
    [SerializeField] private int _maxStashSize = 4;

    private void Start()
    {
        _playerCards = new List<CardController>();
    }

    public void AddCardToStash(CardController newCard)
    {
        if (_playerCards.Count < _maxStashSize)
        {
            Debug.Log("Card Added!");
            _playerCards.Add(newCard);
        }
        else
        {
            Debug.Log("Card not Added!");
        }
    }

    public bool SetSelectedCard(int index)
    {
        CardController card = GetCardAtStash(index);

        if (card == null)
        {
            return false;
        }

        if (_selectedCard == card)
        {
            _selectedCard.DeselectCard();
            _selectedCard = null;
            return false;
        }

        if (_selectedCard != null)
        {
            _selectedCard.DeselectCard();
        }

        _selectedCard = card;
        return true;
    }

    public void CardUsed()
    {
        if (_selectedCard != null)
        {
            int cardIndex = _playerCards.IndexOf(_selectedCard);

            if (cardIndex >= 0)
            {
                _playerCards[cardIndex] = null;
            }
            Destroy(_selectedCard.gameObject);
            _selectedCard = null;
        }
    }

    public CardController GetEquippedCard()
    {
        return _selectedCard;
    }

    public CardController GetCardAtStash(int index)
    {
        return _playerCards[index];
    }
}
