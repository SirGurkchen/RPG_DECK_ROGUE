using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logic of the players card inventory.
/// </summary>
public class PlayerCardStash : MonoBehaviour
{
    [SerializeField] private List<CardController> _playerCards;
    [SerializeField] private CardController _selectedCard;
    [SerializeField] private int _maxStashSize = 4;

    private const int MAX_CARDS = 4;

    private void Start()
    {
        _playerCards = new List<CardController>();
    }

    /// <summary>
    /// Adds a card to the card stash
    /// </summary>
    /// <param name="newCard">Newly added card.</param>
    public void AddCardToStash(CardController newCard)
    {
        if (_playerCards.Count < _maxStashSize)
        {
            _playerCards.Add(newCard);
        }
    }

    /// <summary>
    /// Sets selected card and returns if the selection was succesful.
    /// </summary>
    /// <param name="index">Index of selected card.</param>
    /// <returns>True if selection was succesful.</returns>
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

    /// <summary>
    /// Deselects a card.
    /// </summary>
    public void DeselectCard()
    {
        if (_selectedCard != null)
        {
            _selectedCard.DeselectCard();
            _selectedCard = null;
        }
        Debug.Log(_selectedCard);
    }

    /// <summary>
    /// Handles logic after a card was used.
    /// Removes used card out of player card stash.
    /// </summary>
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

    /// <summary>
    /// Return equipped card.
    /// </summary>
    /// <returns>CardController of equipped card.</returns>
    public CardController GetEquippedCard()
    {
        return _selectedCard;
    }

    /// <summary>
    /// Returns card at index position.
    /// </summary>
    /// <param name="index">Index of position.</param>
    /// <returns>CardController of card at index.</returns>
    public CardController GetCardAtStash(int index)
    {
        return _playerCards[index];
    }

    /// <summary>
    /// Checks if card can be added.
    /// </summary>
    /// <returns>True if another card can be added to card stash</returns>
    public bool CanAdd()
    {
        return _playerCards.Count < MAX_CARDS;
    }

    /// <summary>
    /// Returns card count in stash
    /// </summary>
    /// <returns>Number of cards in stash.</returns>
    public int GetCardCount()
    {
        return _playerCards.Count;
    }
}
