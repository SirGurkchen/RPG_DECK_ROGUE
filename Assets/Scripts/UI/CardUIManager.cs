using UnityEngine;

/// <summary>
/// Visualizes a card object on the screen.
/// Adds and removes cards from their assigned card slots.
/// </summary>
public class CardUIManager : MonoBehaviour
{
    [SerializeField] private Transform[] _cardUIPositions;

    private GameObject[] _cards;

    private void Start()
    {
        _cards = new GameObject[_cardUIPositions.Length];
    }

    /// <summary>
    /// Adds card UI to card stash.
    /// </summary>
    /// <param name="card">Card to add.</param>
    public void FillUIWithCard(CardController card)
    {
        for (int i = 0; i <  _cards.Length; i++)
        {
            if (_cards[i] == null)
            {
                card.gameObject.transform.SetParent(_cardUIPositions[i], false);
                card.gameObject.transform.localPosition = Vector3.zero;
                _cards[i] = card.gameObject;
                break;
            }
        }
    }

    /// <summary>
    /// Removes card UI from card stash.
    /// </summary>
    /// <param name="card">Card to remove.</param>
    public void RemoveCardFromUI(CardController card)
    {
        for (int i = 0; i < _cards.Length; i++)
        {
            if (_cards[i] == card)
            {
                _cards[i] = null;
                break;
            }
        }
    }
}
