using System;
using UnityEngine;

/// <summary>
/// Controls the in game instance of an enemy object.
/// Each new enemy has this script as part of their components.
/// Orchestrates its data and UI representation.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyBase _enemyData;
    [SerializeField] private EnemyUI _myUI;

    private int _currentHealth;
    private int _coinsReward;

    public event Action<EnemyController> OnEnemyDeath;

    public int Coins => _coinsReward;

    private void Start()
    {
        InitializeEnemy();
        _myUI.InitHealthText(_currentHealth, _enemyData);
        _myUI.InitSpecialUI(_enemyData);
    }

    private void InitializeEnemy()
    {
        _currentHealth = _enemyData.Health;
        _coinsReward = _enemyData.Coins;
    }

    public virtual void TakeDamage(int damage, AttackType attack)
    {
        if (attack == AttackType.None)
        {
            _currentHealth -= damage;
            _myUI.UpdateHealthbar(_currentHealth, _enemyData);
        }
        else
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
                _myUI.UpdateHealthbar(_currentHealth, _enemyData);
            }
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
        _myUI.SetEnemeyMarker(isMarked);
    }

    public void Attack(PlayerManager player)
    {
        player.TakeDamage(_enemyData.Damage);
    }

    public string GetEnemyName()
    {
        return _enemyData.Name;
    }

    private void OnDestroy()
    {
        OnEnemyDeath = null;
    }
}
