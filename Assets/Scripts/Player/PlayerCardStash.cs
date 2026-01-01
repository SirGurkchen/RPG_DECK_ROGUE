using System.Collections.Generic;
using UnityEngine;

public class PlayerCardStash : MonoBehaviour
{
    [SerializeField] private List<CardController> _playerCards;
    [SerializeField] private CardController _selectedCard;
    [SerializeField] private int _maxStashSize = 4;

    public void AddCardToStash(CardController newCard)
    {
        if (_playerCards.Count < _maxStashSize)
        {
            _playerCards.Add(newCard);
        }
    }

    public void SetSelectedCard(int index)
    {
        if (_playerCards[index] != null)
        {
            _selectedCard = _playerCards[index];
            Debug.Log("Equipped Card!");
        }
        else
        {
            Debug.Log("No Card Here!");
        }
    }

    public CardController GetEquippedCard()
    {
        return _selectedCard;
    }
}
