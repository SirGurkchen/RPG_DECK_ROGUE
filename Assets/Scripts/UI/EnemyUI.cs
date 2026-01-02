using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private GameObject _marker;
    [SerializeField] private Image _healthbar;
    [SerializeField] private TextMeshProUGUI _enemyHealthUI;
    [Tooltip("Leave empty if Enemy has no block")]
    [SerializeField] private TextMeshProUGUI _enemyBlockUI;

    public void SetEnemeyMarker(bool isMarked)
    {
        _marker.SetActive(isMarked);
    }

    public void InitHealthText(int currentHealth, EnemyBase enemyData)
    {
        _enemyHealthUI.text = currentHealth + " | " + enemyData.Health;

        if (enemyData is IBlock block)
        {
            UpdateBlock(block.Block);
        }
    }

    public void UpdateHealthbar(int currentHealth, EnemyBase enemyData)
    {
        float fill = (float) currentHealth / enemyData.Health;

        _healthbar.fillAmount = Mathf.Clamp01(fill);
        InitHealthText(currentHealth, enemyData);
    }

    private void UpdateBlock(int block)
    {
        if (_enemyBlockUI != null)
        {
            _enemyBlockUI.text = "" + block;
        }
    }
}
