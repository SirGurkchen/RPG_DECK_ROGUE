using TMPro;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Visualizes an instance of a card as its placement and animation.
/// Uses the DOTween Library to support up and down selection animations.
/// </summary>
public class CardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cardNameText;
    [SerializeField] private TextMeshProUGUI _cardDescText;

    private const float SELECT_HEIGHT = 2.5f;
    private bool _selected = false;
    private Vector3 _originalPos;

    private void Start()
    {
        _originalPos = transform.position;
    }

    public void InitCard(CardBase card)
    {
        _cardNameText.text = card.Name;
        _cardDescText.text = card.Description;
    }

    public void VisualizeCardSelect()
    {
        if (!_selected)
        {
            transform.DOKill();
            Vector3 selectPos = _originalPos + new Vector3(0, SELECT_HEIGHT, 0);
            gameObject.transform.DOMove(selectPos, 0.2f);
            _selected = true;
        }
    }

    public void VisualizeCardDeselect()
    {
        transform.DOKill();
        gameObject.transform.DOMove(_originalPos, 0.2f);
        _selected = false;
    }
}
