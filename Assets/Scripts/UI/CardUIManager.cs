using System.Collections.Generic;
using UnityEngine;

public class CardUIManager : MonoBehaviour
{
    [SerializeField] private Transform[] _cardUIPositions;

    private GameObject[] _cards;

    public void Start()
    {
        _cards = new GameObject[_cardUIPositions.Length];
    }

    public void FillUIWithCard(CardController card)
    {
        for (int i = 0; i <  _cards.Length; i++)
        {
            if (_cards[i] == null)
            {
                card.gameObject.transform.SetParent(_cardUIPositions[i]);
                card.gameObject.transform.localPosition = Vector3.zero;
                _cards[i] = card.gameObject;
                break;
            }
        }
    }

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
