using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyBase _enemyData;
    [SerializeField] private GameObject _marker;

    private int _currentHealth;

    public event Action<EnemyController> OnEnemyDeath;

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
        OnEnemyDeath?.Invoke(this);
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

    public void SetEnemeyMarker(bool isMarked)
    {
        _marker.SetActive(isMarked);
    }

    private void OnDestroy()
    {
        OnEnemyDeath = null;
    }
}
