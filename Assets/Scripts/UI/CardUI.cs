using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cardNameText;
    [SerializeField] private TextMeshProUGUI _cardDescText;


    public void InitCard(CardBase card)
    {
        _cardNameText.text = card.Name;
        _cardDescText.text = card.Description;
    }
}
