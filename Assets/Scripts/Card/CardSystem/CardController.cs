using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardBase _card;

    public CardBase GetCard()
    {
        return _card;
    }
}
