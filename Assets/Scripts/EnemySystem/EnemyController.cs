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

    public virtual void TakeDamage(int damage, AttackType attack)
    {
        int finalDamage = damage;

        finalDamage *= GetWeakness(attack);

        if (_enemyData is IBlock blocker)
        {
            finalDamage -= blocker.Block;
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
        Destroy(gameObject);
    }

    public string GetEnemyStats()
    {
        return _enemyData.GetEnemyStats() + " Health: " + _currentHealth;
    }

    private int GetWeakness(AttackType attack)
    {
        if (attack == _enemyData.Weakness)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
