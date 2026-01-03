using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardBase _card;
    [SerializeField] private CardUI _myUI;

    private void Start()
    {
        _myUI.InitCard(_card);
    }

    public CardBase GetCard()
    {
        return _card;
    }
}
