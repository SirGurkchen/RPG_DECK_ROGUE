using JetBrains.Annotations;
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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
        _myUI.SetEnemyImage(_enemyData.Icon);
    }

    private void InitializeEnemy()
    {
        _currentHealth = _enemyData.Health;
        _coinsReward = _enemyData.Coins;
    }

    public virtual void TakeDamage(int damage, AttackType attack)
    {
        if (TryDodge()) return;

        if (attack == AttackType.None)
        {
            _currentHealth -= damage;
            _myUI.UpdateHealthbar(_currentHealth, _enemyData);
        }
        else
        {
            int calcDamage = CalculateDamage(damage, attack);
            if (calcDamage >= 0)
            {
                _currentHealth -= calcDamage;
                _myUI.UpdateHealthbar(_currentHealth, _enemyData);
            }
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private int CalculateDamage(int damage, AttackType attack)
    {
        int finalDamage = damage;

        finalDamage *= GetWeakness(attack);

        if (_enemyData is IBlock blocker)
        {
            finalDamage -= blocker.Block;
        }
        return finalDamage;
    }

    private void Die()
    {
        OnEnemyDeath?.Invoke(this);
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

    private bool TryDodge()
    {
        if (_enemyData is IDodge dodge)
        {
            int rng = UnityEngine.Random.Range(0, dodge.DodgeChance);
            if (rng == 0)
            {
                Debug.Log("Dodge!");
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
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
