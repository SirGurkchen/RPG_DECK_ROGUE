using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the visualization of an enemy.
/// Uses the EnemeStatusUI to dynamically show/hide potential enemy statuses.
/// </summary>
public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image _healthbar;
    [SerializeField] private TextMeshProUGUI _enemyHealthUI;
    [SerializeField] private EnemyStatusUI _enemyStatusUI;
    [SerializeField] private Image _enemyImage;

    /// <summary>
    /// Sets the activity of the enemy marker.
    /// </summary>
    /// <param name="isMarked">Activity of marker.</param>
    public void SetEnemeyMarker(bool isMarked)
    {
        if (isMarked)
        {
            _enemyImage.color = Color.red;
        }
        else
        {
            _enemyImage.color = Color.white;
        }
    }

    /// <summary>
    /// Sets the image of the enemy.
    /// </summary>
    /// <param name="sprite">Image to set.</param>
    public void SetEnemyImage(Sprite sprite)
    {
        if (_enemyImage != null && sprite != null)
        {
            _enemyImage.sprite = sprite;
        }
        else
        {
            Debug.Log("Image did not Load!");
        }
    }

    /// <summary>
    /// Sets health text.
    /// </summary>
    /// <param name="currentHealth">Current health.</param>
    /// <param name="enemyData">Enemy data.</param>
    public void InitHealthText(int currentHealth, EnemyBase enemyData)
    {
        _enemyHealthUI.text = currentHealth + " | " + enemyData.Health;
    }

    /// <summary>
    /// Updates the health bar.
    /// </summary>
    /// <param name="currentHealth">Current health.</param>
    /// <param name="enemyData">Enemy data.</param>
    public void UpdateHealthbar(int currentHealth, EnemyBase enemyData)
    {
        float fill = (float) currentHealth / enemyData.Health;

        _healthbar.fillAmount = Mathf.Clamp01(fill);
        InitHealthText(currentHealth, enemyData);
    }

    /// <summary>
    /// Sets the enemy status UI.
    /// </summary>
    /// <param name="enemy">Enemy data.</param>
    public void InitSpecialUI(EnemyBase enemy)
    {
        _enemyStatusUI.UpdateStatus(enemy);
    }

    /// <summary>
    /// Updates the thorn display.
    /// </summary>
    /// <param name="thorns">Number of thorns.</param>
    public void UpdateThornsDisplay(int thorns)
    {
        _enemyStatusUI.UpdateThorns(thorns);
    }
}
