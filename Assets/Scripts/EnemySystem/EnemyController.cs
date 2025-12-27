using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyBase _enemyData;

    private int _currentHealth;

    private void Start()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        _currentHealth = _enemyData.Health;
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = damage;

        if (_enemyData is IBlock blocker)
        {
            finalDamage = damage - blocker.Block;
        }

        if (finalDamage >= 0)
        {
            _currentHealth -= finalDamage;
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        print("Enemy Died!");
    }

    public string GetEnemyStats()
    {
        return _enemyData.GetEnemyStats() + " Health: " + _currentHealth;
    }
}
