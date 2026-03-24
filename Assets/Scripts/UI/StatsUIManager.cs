using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the updating of the status UI of enemies.
/// </summary>
public class StatsUIManager : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthBarText;
    [SerializeField] private Image _manaBar;
    [SerializeField] private TextMeshProUGUI _manaBarText;
    [SerializeField] private TextMeshProUGUI _coinsUI;

    /// <summary>
    /// Updates health bar.
    /// </summary>
    /// <param name="health">Current health.</param>
    /// <param name="maxHealth">Maximum health.</param>
    public void UpdateHealthBar(int health, int maxHealth)
    {
        float fill = (float)health / maxHealth;

        _healthBar.fillAmount = Mathf.Clamp01(fill);
        _healthBarText.text = health + " | " + maxHealth;
    }

    /// <summary>
    /// Updates mana bar.
    /// </summary>
    /// <param name="mana">Current mana.</param>
    /// <param name="maxMana">Maximum mana.</param>
    public void UpdateManaUI(int mana, int maxMana)
    {
        float fill = (float)mana / maxMana;

        _manaBar.fillAmount = Mathf.Clamp01(fill);
        _manaBarText.text = mana + " | " + maxMana;
    }

    /// <summary>
    /// Updates coin UI
    /// </summary>
    /// <param name="coins">Current coins.</param>
    public void UpdateCoinsUI(int coins)
    {
        _coinsUI.text = "" + coins;
    }
}
