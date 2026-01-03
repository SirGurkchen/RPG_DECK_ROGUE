using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weaponUI;
    [SerializeField] private TextMeshProUGUI _healthUI;
    [SerializeField] private CardUIManager _cardUI;

    private string _defaultHealthText;

    private void Start()
    {
        _defaultHealthText = _healthUI.text;
    }

    public void UpdateWeaponUI(ItemController item)
    {
        _weaponUI.text = item.GetItemName();
    }

    public void ClearWeaponUI()
    {
        _weaponUI.text = "";
    }

    public void UpdateHealthText(int health, int maxHealth)
    {
        _healthUI.text = _defaultHealthText + health + " | " + maxHealth;
    }

    public void AddCardUI(CardController card)
    {
        _cardUI.FillUIWithCard(card);
    }

    public void RemoveCardUI(CardController card)
    {
        _cardUI.RemoveCardFromUI(card);
    }
}
