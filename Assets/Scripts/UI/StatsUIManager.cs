using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIManager : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthBarText;
    [SerializeField] private Image _manaBar;
    [SerializeField] private TextMeshProUGUI _manaBarText;
    [SerializeField] private TextMeshProUGUI _coinsUI;

    public void UpdateHealthBar(int health, int maxHealth)
    {
        float fill = (float)health / maxHealth;

        _healthBar.fillAmount = Mathf.Clamp01(fill);
        _healthBarText.text = health + " | " + maxHealth;
    }

    public void UpdateManaUI(int mana, int maxMana)
    {
        float fill = (float)mana / maxMana;

        _manaBar.fillAmount = Mathf.Clamp01(fill);
        _manaBarText.text = mana + " | " + maxMana;
    }

    public void UpdateCoinsUI(int coins)
    {
        _coinsUI.text = "" + coins;
    }
}
