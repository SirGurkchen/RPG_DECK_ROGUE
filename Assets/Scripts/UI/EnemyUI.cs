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

    public void InitHealthText(int currentHealth, EnemyBase enemyData)
    {
        _enemyHealthUI.text = currentHealth + " | " + enemyData.Health;
    }

    public void UpdateHealthbar(int currentHealth, EnemyBase enemyData)
    {
        float fill = (float) currentHealth / enemyData.Health;

        _healthbar.fillAmount = Mathf.Clamp01(fill);
        InitHealthText(currentHealth, enemyData);
    }

    public void InitSpecialUI(EnemyBase enemy)
    {
        _enemyStatusUI.UpdateStatus(enemy);
    }
}
