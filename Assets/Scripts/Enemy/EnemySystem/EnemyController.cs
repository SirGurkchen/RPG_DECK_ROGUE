using System;
using UnityEngine;

/// <summary>
/// Controls the in game instance of an enemy object.
/// Each new enemy has this script as part of their components.
/// Orchestrates its data and UI representation.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [Header("Base Info. Available to subclasses")]
    [SerializeField] protected EnemyBase _enemyData;
    [SerializeField] protected EnemyUI _myUI;

    [Header("Visual Info")]
    [Tooltip("Leave empty if no attack sound")]
    [SerializeField] private AudioClip _attackSound;
    [Tooltip("Leave empty if no spawn sound")]
    [SerializeField] private AudioClip _spawnSound;
    [SerializeField] private EnemyAnimator _animator;

    private int _currentHealth;
    private int _coinsReward;

    public event Action<EnemyController> OnEnemyDeath;

    public int Coins => _coinsReward;
    public string GetEnemyName() => _enemyData.Name;


    private void Start()
    {
        InitializeEnemy();
        _myUI.InitHealthText(_currentHealth, _enemyData);
        _myUI.InitSpecialUI(_enemyData);
        _myUI.SetEnemyImage(_enemyData.Icon);
    }

    protected virtual void InitializeEnemy()
    {
        _currentHealth = _enemyData.Health;
        _coinsReward = _enemyData.Coins;
        _animator.SetOriginalPos(gameObject.transform.position);
        AudioManager.Instance.PlayAudioClip(_spawnSound);
    }

    public virtual void TakeDamage(int damage, AttackType attack)
    {
        GetDamaged(damage, attack);
        AfterDamaged();
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
        return attack == _enemyData.Weakness ? 2 : 1;
    }

    private void AfterDamaged()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            _animator.PlayDamagingAnimation();
        }
    }

    private void GetDamaged(int damage, AttackType attack)
    {
        int finalDamange = attack == AttackType.None ? damage : CalculateDamage(damage, attack);
        if (finalDamange >= 0)
        {
            _currentHealth -= finalDamange;
            _myUI.UpdateHealthbar(_currentHealth, _enemyData);
        }
    }

    public void SetEnemeyMarker(bool isMarked)
    {
        _myUI.SetEnemeyMarker(isMarked);
    }

    public virtual void Attack(PlayerManager player)
    {
        _animator.PlayAttackAnimation();
        AudioManager.Instance.PlayAudioClip(_attackSound);
        player.TakeDamage(_enemyData.Damage);
    }

    private void OnDestroy()
    {
        OnEnemyDeath = null;
    }
}
