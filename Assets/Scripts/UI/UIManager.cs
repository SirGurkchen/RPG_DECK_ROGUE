using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weaponUI;
    [SerializeField] private TextMeshProUGUI _healthUI;
    [SerializeField] private TextMeshProUGUI _selectCardUI;

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

    public void UpdateCardUI(CardBase card)
    {
        _selectCardUI.text = card.GetCardName();
    }

    public void ClearCardUI()
    {
        _selectCardUI.text = "";
    }

    public void UpdateHealthText(int health, int maxHealth)
    {
        _healthUI.text = _defaultHealthText + health + " | " + maxHealth;
    }
}
