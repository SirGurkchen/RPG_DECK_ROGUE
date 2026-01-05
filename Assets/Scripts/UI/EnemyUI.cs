using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private GameObject _marker;
    [SerializeField] private Image _healthbar;
    [SerializeField] private TextMeshProUGUI _enemyHealthUI;
    [SerializeField] private EnemyStatusUI _enemyStatusUI;

    public void SetEnemeyMarker(bool isMarked)
    {
        _marker.SetActive(isMarked);
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
