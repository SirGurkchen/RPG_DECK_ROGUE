using System;
using UnityEngine;

/// <summary>
/// Handles the player stats.
/// Updates only the logical components.
/// Checks for player death.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _armor;
    [SerializeField] private int _maxMana;

    private int _health;
    private int _mana;
    private int _coins;

    public event Action OnPlayerDeath;
    public event Action OnPlayerHeal;
    public event Action OnManaChange;

    public int MaxHealth => _maxHealth;
    public int Health => _health;
    public int MaxMana => _maxMana;
    public int Mana => _mana;
    public int Coins => _coins;

    private void Start()
    {
        _health = _maxHealth;
        _mana = _maxMana;
        _coins = 0;
    }

    /// <summary>
    /// Receives damage.
    /// </summary>
    /// <param name="damage">Damage to receive.</param>
    public void ReceiveDamage(int damage)
    {
        int finalDamage = damage - _armor;

        if (finalDamage > 0)
        {
            _health -= finalDamage;
            if (_health < 0)
            {
                _health = 0;
            }
        }

        CheckDeath();
    }

    /// <summary>
    /// Heals the player.
    /// </summary>
    /// <param name="healAmount">Amount to heal player.</param>
    public void HealPlayer(int healAmount)
    {
        if (_health < _maxHealth)
        {
            _health += healAmount;
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            OnPlayerHeal?.Invoke();
        }
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }

    /// <summary>
    /// Removes mana from player.
    /// </summary>
    /// <param name="amount">Amount to remove.</param>
    public void RemoveMana(int amount)
    {
        _mana -= amount;
        OnManaChange?.Invoke();
    }

    /// <summary>
    /// Adds mana to the player.
    /// </summary>
    /// <param name="amount">Amount to add.</param>
    public void AddMana(int amount)
    {
        if (_mana < _maxMana)
        {
            _mana += amount;

            if (_mana > _maxMana)
            {
                _mana = _maxMana;
            }
        }
    }

    /// <summary>
    /// Adds coins to player.
    /// </summary>
    /// <param name="amount">Coins to add.</param>
    public void AddCoins(int amount)
    {
        _coins += amount;
    }

    /// <summary>
    /// Removes coins from player.
    /// </summary>
    /// <param name="amount">Coins to remove.</param>
    public void RemoveCoins(int amount)
    {
        _coins -= amount; 
    }

    private void OnDestroy()
    {
        OnPlayerDeath = null;
        OnPlayerHeal = null;
        OnManaChange = null;
    }
}
