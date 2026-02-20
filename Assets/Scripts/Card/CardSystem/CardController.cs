using UnityEngine;

/// <summary>
/// Controls the In Game Card and its visualization.
/// This is achieved by orchestration of CardBase Data and the CardUI class.
/// </summary>
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

    public void SelectCard()
    {
        _myUI.VisualizeCardSelect();
    }

    public void DeselectCard()
    {
        _myUI.VisualizeCardDeselect();
    }
}
